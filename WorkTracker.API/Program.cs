using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.IO.Compression;
using WorkTracker.API.Extensions;
using WorkTracker.Core.Converters;
using WorkTracker.Core.Interfaces;
using WorkTracker.Core.Models;
using WorkTracker.Data.Context;
using WorkTracker.Data.Extensions;
using WorkTracker.Services.Infrastructure;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Init application Work-Tracker");
    var builder = WebApplication.CreateBuilder(args);
    const string CorsPolicy = "CorsPolicy";

    // Configure and add services to the container.

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        // Configure a custom converter
        options.SerializerSettings.Converters.Add(new DateOnlyConverter());
    });
    builder.Services.AddMapper();
    builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("1.0", new OpenApiInfo { Title = "Work-Tracker API", Version = "1.0" });
        List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
    });

    builder.Services.AddRepositories();
    builder.Services.AddServices();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(CorsPolicy,
            policy => policy
                .WithOrigins()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });

    // Configure DB connection

    var dbSection = builder.Configuration.GetSection(nameof(ConnectionStrings));
    builder.Services.Configure<ConnectionStrings>(dbSection);

    builder.Services.AddSingleton<IConnectionStrings>(sp =>
        sp.GetRequiredService<IOptions<ConnectionStrings>>().Value);

    var connection = dbSection.Get<ConnectionStrings>();

    builder.Services.AddDbContext<WorkTrackerContext>(options =>
    {
        options.UseNpgsql(connection.PostgreSQLConnection, b => b.MigrationsAssembly("WorkTracker.Data"));
    });

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();

    Migrate(app);

    app.UseResponseCompression();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
        app.UseHsts();

    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/1.0/swagger.json", "API 1.0"); });

    app.UseCors(CorsPolicy);

    app.UseRouting();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    //NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

void Migrate(IApplicationBuilder app)
{
    using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<WorkTrackerContext>();
        context.Database.Migrate();
        logger.Info("Database migrate success");
    }
}
