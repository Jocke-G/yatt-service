using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YattService.Persistance
{
    public class YattDbContextFactory : IDesignTimeDbContextFactory<YattDbContext>
    {
        public YattDbContext CreateDbContext(string[] args)
        {
            // Bygg config — pekar upp två nivåer till API-projektet typiskt
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Yatt-Service");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<YattDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new YattDbContext(optionsBuilder.Options);
        }
    }
}
