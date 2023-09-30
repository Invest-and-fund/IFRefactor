using AcmeStudios.ApiRefactor.Domain;
using AcmeStudios.ApiRefactor.DTOs;
using AutoMapper;

namespace AcmeStudios.ApiRefactor
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudioItem, GetStudioItemDto>();
            CreateMap<AddStudioItemDto, StudioItem>();
            CreateMap<StudioItem, GetStudioItemHeaderDto>();
            CreateMap<StudioItemType, StudioItemTypeDto>()
                .ReverseMap();

            CreateMap<UpdateStudioItemDto, StudioItem>()
                .ForMember(dest => dest.StudioItemTypeId, opt => opt.MapFrom(src => src.StudioItemType.StudioItemTypeId));
        }
    }
}