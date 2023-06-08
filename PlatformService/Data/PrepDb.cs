using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, isProduction);
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it in some other way
                Console.WriteLine($"Error populating database: {ex.Message}");
                throw;
            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> PRODUCTION - Iniciando la migración de datos...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> PRODUCTION - Error en la migración de datos: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.Platforms.AddRange(
                    new Platform() {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() {Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() {Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}