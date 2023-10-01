using AcmeStudios.ApiRefactor.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeStudios.ApiRefactor.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string dbConnectionString)
        {
            services.AddScoped<IStudioItemRepository, StudioItemRepository>();
            services.AddScoped<IStudioItemTypeRepository, StudioItemTypeRepository>();

            services.AddDbContext<Cont>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            return services;
        }
    }
}
