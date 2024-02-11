using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;

using AutoMapper;

namespace AcmeStudios.ApiRefactor.Profiles
{
    public class StudioItemTypeProfile : Profile
    {
        public StudioItemTypeProfile()
        {
            CreateMap<StudioItemTypeDto, StudioItemType>()
                .ReverseMap();
        }
    }
}
