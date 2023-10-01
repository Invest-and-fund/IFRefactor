using AcmeStudios.ApiRefactor.Application.DTOs;
using AcmeStudios.ApiRefactor.DataAccess.Repositories;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Application.Services
{
    public sealed class StudioItemTypeService : IStudioItemTypeService
    {
        private readonly IStudioItemTypeRepository _studioItemTypeRepository;
        private readonly IMapper _mapper;

        public StudioItemTypeService(IStudioItemTypeRepository studioItemTypeRepository, IMapper mapper)
        {
            _studioItemTypeRepository = studioItemTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<StudioItemTypeDto>>> GetAllStudioItemTypesAsync()
        {
            var studioItems = await _studioItemTypeRepository.GetAllAsync();

            return ServiceResponse<IEnumerable<StudioItemTypeDto>>
                .Succeeded(_mapper.Map<IEnumerable<StudioItemTypeDto>>(studioItems), "Item types fetched");
        }
    }
}
