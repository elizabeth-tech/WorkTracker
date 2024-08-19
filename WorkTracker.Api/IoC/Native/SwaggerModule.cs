using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WorkTracker.Api.IoC.Native
{
    /// <summary>
    /// Модуль DI для Swagger
    /// </summary>
    public static class SwaggerModule
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        /// <param name="services">Коллекция дескрипторов служб</param>
        public static void Load(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "WorkTracker.Api",
                        Version = "v1",
                        Description = "Документация WorkTracker API"
                    });

                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly
                    .GetReferencedAssemblies()
                    .Union(new AssemblyName[]
                    {
                        currentAssembly.GetName()
                    })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, d => options.IncludeXmlComments(d, true));
            });
        }
    }
}
