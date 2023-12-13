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

            string conn = _configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(conn);

        }

    }
}

