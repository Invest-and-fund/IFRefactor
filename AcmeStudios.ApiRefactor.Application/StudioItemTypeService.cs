using AcmeStudios.ApiRefactor.Application.DTOs;
using AcmeStudios.ApiRefactor.Application.Services;
using AcmeStudios.ApiRefactor.DataAccess.Repositories;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Application
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

            var serviceResponse = new ServiceResponse<IEnumerable<StudioItemTypeDto>>
            {
                Data = _mapper.Map<IEnumerable<StudioItemTypeDto>>(studioItems),
                Message = "Item types fetched",
                Success = true
            };

            return serviceResponse;
        }
    }
}
