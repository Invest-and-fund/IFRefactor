using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DataAccess.Repositories;
using AcmeStudios.ApiRefactor.Domain;
using AcmeStudios.ApiRefactor.DTOs;
using AutoMapper;

namespace AcmeStudios.ApiRefactor
{
    public class InterfaceWithDatabase
    {
        private readonly IStudioItemRepository _studioItemRepository;
        private readonly IStudioItemTypeRepository _studioItemTypeRepository;
        private readonly IMapper _mapper;

        public InterfaceWithDatabase(IStudioItemRepository studioItemRepository, IStudioItemTypeRepository studioItemTypeRepository, IMapper mapper)
        {
            _studioItemRepository = studioItemRepository;
            _studioItemTypeRepository = studioItemTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
            await _studioItemRepository.AddAsync(item);

            var serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = _mapper.Map<GetStudioItemDto>(item),
                Message = $"New item added.  Id: {item.StudioItemId}",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>> GetAllStudioItemHeaders()
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

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
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

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
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

        public async Task<ServiceResponse<int>> DeleteStudioItem(int id)
        {
            var serviceResponse = new ServiceResponse<int>();

            var success = await _studioItemRepository.RemoveAsync(id);

            serviceResponse.Data = success ? id : default;
            serviceResponse.Success = success;
            serviceResponse.Message = success ? "Item deleted" : "Unexpected error";

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<StudioItemType>>> GetAllStudioItemTypes()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<StudioItemType>>
            {
                Data = await _studioItemTypeRepository.GetAllAsync(),
                Message = "Item types fetched",
                Success = true
            };

            return serviceResponse;
        }
    }

    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }

}
