using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeStudios.ApiRefactor.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<Cont>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            return services;
        }
    }
}
