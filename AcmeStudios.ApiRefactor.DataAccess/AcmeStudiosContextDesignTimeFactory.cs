using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AcmeStudios.ApiRefactor.DataAccess
{
    internal sealed class AcmeStudiosContextDesignTimeFactory : IDesignTimeDbContextFactory<AcmeStudiosContext>
    {
        public AcmeStudiosContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<AcmeStudiosContext>();
            var connectionString = configuration.GetConnectionString("StudioConnection");

            if (connectionString is not null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            return new AcmeStudiosContext(optionsBuilder.Options);
        }
    }
}
