using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace IncidentAPI.Models;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices((context, services) =>
        {
            // Supprimer l'ancien DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<IncidentsDbContext>));
            
            if (descriptor != null)
                services.Remove(descriptor);

            var connectionString = context.Configuration.GetConnectionString("IncidentsConnection")
                ?? "Server=(localdb)\\mssqllocaldb;Database=IncidentDb_Test;Trusted_Connection=True;TrustServerCertificate=True;";

            // Ajouter un DbContext avec BD de test
            services.AddDbContext<IncidentsDbContext>(options => 
                options.UseSqlServer(connectionString));

            // Construire le provider
            var sp = services.BuildServiceProvider();

            // Initialiser la BD
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IncidentsDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        });
    }
}