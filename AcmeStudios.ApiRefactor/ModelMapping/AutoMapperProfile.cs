using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Repositories;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.ModelMapping;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StudioItem, GetStudioItemDto>();
        CreateMap<AddStudioItemDto, StudioItem>();
        CreateMap<StudioItem, GetStudioItemHeaderDto>();
    }
}