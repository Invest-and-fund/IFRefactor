using AcmeStudios.ApiRefactor.Application.DTOs;
using AcmeStudios.ApiRefactor.DataAccess.Repositories;
using AcmeStudios.ApiRefactor.Domain;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Application.Services
{
    public sealed class StudioItemService : IStudioItemService
    {
        private readonly IStudioItemRepository _studioItemRepository;
        private readonly IMapper _mapper;

        public StudioItemService(IStudioItemRepository studioItemRepository, IMapper mapper)
        {
            _studioItemRepository = studioItemRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem)
        {
            var item = _mapper.Map<StudioItem>(newStudioItem);
            await _studioItemRepository.AddAsync(item);

            var serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = _mapper.Map<GetStudioItemDto>(item),
                Message = $"New item added.  Id: {item.StudioItemId}",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> DeleteStudioItemAsync(int id)
        {
            var serviceResponse = new ServiceResponse<int>();

            var success = await _studioItemRepository.RemoveAsync(id);

            serviceResponse.Data = success ? id : default;
            serviceResponse.Success = success;
            serviceResponse.Message = success ? "Item deleted" : "Unexpected error";

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>> GetAllStudioItemHeadersAsync()
        {
            var items = await _studioItemRepository.GetAllAsync();

            var serviceResponse = new ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>
            {
                Data = _mapper.Map<IEnumerable<GetStudioItemHeaderDto>>(items),
                Message = "Here's all the items in your studio",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemByIdAsync(int id)
        {
            var item = await _studioItemRepository.GetByIdAsync(id);

            var serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = _mapper.Map<GetStudioItemDto>(item),
                Message = "Here's your selected studio item",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem)
        {
            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            // TODO add whatever validation needs to take place here and return unsuccessful responses if so

            var studioItem = _mapper.Map<StudioItem>(updatedStudioItem);

            var successful = await _studioItemRepository.UpdateAsync(studioItem);

            if (successful)
            {
                serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItem);
                serviceResponse.Message = "Update successful";
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Unexpected error";
            }

            return serviceResponse;
        }
    }
}
