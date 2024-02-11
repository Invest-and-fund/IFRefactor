using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;

using AutoMapper;

namespace AcmeStudios.ApiRefactor.Profiles
{
    public class StudioItemProfile : Profile
    {
        public StudioItemProfile()
        {
            CreateMap<StudioItem, StudioItemDto>();
            CreateMap<StudioItemForCreationDto, StudioItem>();
            CreateMap<StudioItem, StudioItemHeaderDto>();
            CreateMap<StudioItemForUpdateDto, StudioItem>();
        }
    }
}