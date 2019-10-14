using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Documents.Manager.Service.Extensions
{
    /// <summary>
    /// Startup extensions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class StartupExtensions
    {
        #region :: Methods ::

        /// <summary>
        /// Adds the swagger.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Resources Manager",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Conekta Engineering Team",
                        Email = "engineering@conekta.com"
                    }
                });
            });
        }

        /// <summary>
        /// Adds the cors.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void AddCORS(this IServiceCollection services, IConfiguration configuration)
        {
            var originsEnabled = configuration["OriginsEnabled"];

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOriginsHeadersAndMethods",
                    builder => builder.WithOrigins(originsEnabled)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        #endregion
    }
}
