using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Documents.Manager.Models.Models;
using Documents.Manager.Service.Exceptions;
using Documents.Manager.Service.Extensions;
using Documents.Manager.Service.Models;

namespace Documents.Manager.Service
{
    /// <summary>
    /// Startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The current environment.
        /// </summary>
        private readonly IHostingEnvironment _currentEnvironment;

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="env">Env.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">Services.</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = string.Empty;
            var servicesUrls = new Dictionary<string, string>();
            Dictionary<string, string> secrets = new Dictionary<string, string>();

            if (_currentEnvironment.IsEnvironment("Testing") || _currentEnvironment.IsDevelopment())
            {
                connection = Environment.GetEnvironmentVariable("CONNECTION");
            }
            else if (_currentEnvironment.IsStaging() || _currentEnvironment.IsProduction())
            {
                secrets = JsonConvert.DeserializeObject<Dictionary<string, string>>(SecretsManager.GetSecret());
                connection = secrets["CONNECTION"];
            }

            services.AddEntityFrameworkNpgsql().AddDbContext<DocumentsManagerDbContext>(
                opt => opt.UseNpgsql(connection,
                target => target.MigrationsAssembly("Documents.Manager.Models")));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore
            );
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;

                    // automatically applies an api version based on the name of the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                });
            services.AddSingleton(servicesUrls);
            services.Configure<CustomErrors>(options => Configuration.GetSection("CustomErrors").Bind(options));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddHealthChecks()
                .AddNpgSql(connection, tags: new[] { "complete" });

            services.AddCORS(Configuration);
            services.AddLogging();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            if (_currentEnvironment.IsDevelopment())
            {
                services.AddSwagger();
            }
        }

        /// <summary>
        /// Configure the specified app and env.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Env.</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DocumentsManagerDbContext>();

                if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documents Manager Service");
                });
            }

            app.UseCors("AllowOriginsHeadersAndMethods");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();
            app.UseMvc();

            var options = new HealthCheckOptions();
            options.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
            options.ResponseWriter = HealthResponseWriter;
            app.UseHealthChecks("/health", options);

            var pingOptions = new HealthCheckOptions();
            pingOptions.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
            pingOptions.ResponseWriter = HealthResponseWriter;
            pingOptions.Predicate = (check) => !check.Tags.Contains("complete") && !check.Tags.Contains("ready");
            app.UseHealthChecks("/ping", pingOptions) ;

            app.UseCors("AllowOriginsHeadersAndMethods");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// Health response writer.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="healthReport">Health report.</param>
        /// <returns>The async.</returns>
        protected async Task HealthResponseWriter(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json";

            if (healthReport.Status.Equals(HealthStatus.Healthy))
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    status = HttpStatusCode.OK.ToString(),
                    version = GetAssemblyVersion()
                }));
            }
            else if (healthReport.Status.Equals(HealthStatus.Unhealthy))
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    status = "internal_server_error",
                    version = GetAssemblyVersion(),
                    errors = healthReport.Entries
                }));
            }
            else
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    status = "internal_server_error",
                    version = GetAssemblyVersion()
                }));
            }
        }

        /// <summary>
        /// Get the version of the assembly.
        /// </summary>
        /// <returns>Version.</returns>
        protected string GetAssemblyVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        #endregion
    }
}
