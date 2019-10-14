using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories
{
    /// <summary>
    /// Custom web application factory.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CustomWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
    {
        #region  Override Methods 

        /// <summary>
        /// Configures the web host.
        /// </summary>
        /// <param name="builder">Builder.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory
                // database for testing.
                services.AddDbContext<DocumentsManagerDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DocumentsManagerTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<DocumentsManagerDbContext>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                }
            });

            builder.UseEnvironment("Testing");
        }

        #endregion
    }
}
