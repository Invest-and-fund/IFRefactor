using AcemStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.DomainModels;
using AutoMapper;

namespace AcemStudios.ApiRefactor
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudioItem, GetStudioItemDto>();
            CreateMap<AddStudioItemDto, StudioItem>();
            CreateMap<StudioItem, GetStudioItemHeaderDto>();
        }
    }
}