using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeStudios.ApiRefactor.Data;

public static class DatabaseConfig
{
    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EFStudioDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("StudioConnection"))
        );
    }
}