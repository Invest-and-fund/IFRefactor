using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.ModelMapping;
using AcmeStudios.ApiRefactor.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeStudios.ApiRefactor.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAcmeDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton(MapperFactory.CreateMapper())
            .AddSingleton<InterfaceWithDatabase>();
    }

    public static IServiceCollection AddDataBaseConnection(this IServiceCollection serviceCollection, string connectionString)
    {
        return serviceCollection
            .AddDbContext<Cont>(opts => opts.UseSqlServer(connectionString));
    }
}