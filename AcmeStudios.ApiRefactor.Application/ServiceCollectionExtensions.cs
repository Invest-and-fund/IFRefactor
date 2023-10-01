using AcmeStudios.ApiRefactor.Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeStudios.ApiRefactor.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IStudioItemService, StudioItemService>();
            services.AddTransient<IStudioItemTypeService, StudioItemTypeService>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
