using AutoMapper;

namespace AcmeStudios.ApiRefactor.ModelMapping;

public static class MapperFactory
{
    public static IMapper CreateMapper()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }).CreateMapper();
    }
}