using EcoControl.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EcoControl.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var settings = new Dictionary<string, string?>
            {
                ["BasicAuth:AdminUser"] = "admin-test",
                ["BasicAuth:AdminPassword"] = "admin-test-password",
                ["BasicAuth:ViewerUser"] = "viewer-test",
                ["BasicAuth:ViewerPassword"] = "viewer-test-password",
                ["Database:ApplyMigrations"] = "false"
            };

            config.AddInMemoryCollection(settings);
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase($"EcoControlTests-{Guid.NewGuid()}");
            });
        });
    }
}
