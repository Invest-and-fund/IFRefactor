using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AcemStudios.ApiRefactor.Data;

namespace AcemStudios.ApiRefactor
{
    public class DatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptions<Cont> GetDbContextOptions()
        {
            string conn = _configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            return optionsBuilder.Options;
        }
    }
}
