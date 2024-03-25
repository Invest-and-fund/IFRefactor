using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StudioItem, GetStudioItemDto>();
        CreateMap<AddStudioItemDto, StudioItem>();
        CreateMap<StudioItem, GetStudioItemHeaderDto>();
    }
}