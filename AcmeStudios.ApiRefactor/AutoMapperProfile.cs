using AcmeStudios.ApiRefactor.Models;
using AcmeStudios.ApiRefactor.Models.DTOs;
using AcmeStudios.ApiRefactor.Models.DTOs.AcmeStudios.ApiRefactor.Models.Dto;
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
            CreateMap<UpdateStudioItemDto, StudioItem>();
            CreateMap<StudioItemType, StudioItemTypeDto>();
        }
    }
}