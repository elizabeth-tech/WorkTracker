using Autofac;
using Autofac.Extensions.DependencyInjection;
using WorkTracker.Api.IoC.Autofac;
using WorkTracker.Api.IoC.Native;

var builder = WebApplication.CreateBuilder(args);

_ = new NativeAppModule(builder.Services, builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AppModule());
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
