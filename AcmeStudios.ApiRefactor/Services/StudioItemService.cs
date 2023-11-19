using AcmeStudios.ApiRefactor.Models;
using AcmeStudios.ApiRefactor.Models.DTOs;
using AcmeStudios.ApiRefactor.Models.DTOs.AcmeStudios.ApiRefactor.Models.Dto;
using AcmeStudios.ApiRefactor.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Services
{
    public class StudioItemService : IStudioItemService
    {
        private readonly IStudioItemRepository _studioItemRepository;
        private readonly IMapper _mapper;

        public StudioItemService(IStudioItemRepository studioItemRepository, IMapper mapper)
        {
            _studioItemRepository = studioItemRepository;
            _mapper = mapper;
        }

        public async Task<List<GetStudioItemDto>> GetAllStudioItemsAsync()
        {
            var studioItems = await _studioItemRepository.GetAllAsync();
            return _mapper.Map<List<GetStudioItemDto>>(studioItems);
        }

        public async Task<GetStudioItemDto> GetStudioItemByIdAsync(int id)
        {
            var studioItem = await _studioItemRepository.GetByIdAsync(id);
            if (studioItem == null)
            {
                throw new KeyNotFoundException("Studio item not found.");
            }
            return _mapper.Map<GetStudioItemDto>(studioItem);
        }

        public async Task<GetStudioItemDto> AddStudioItemAsync(AddStudioItemDto newStudioItem)
        {
            var studioItem = _mapper.Map<StudioItem>(newStudioItem);
            studioItem = await _studioItemRepository.AddAsync(studioItem);
            return _mapper.Map<GetStudioItemDto>(studioItem);
        }

        public async Task<GetStudioItemDto> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem)
        {
            var studioItem = await _studioItemRepository.GetByIdAsync(updatedStudioItem.StudioItemId);
            if (studioItem == null)
            {
                throw new KeyNotFoundException("Studio item not found.");
            }
            _mapper.Map(updatedStudioItem, studioItem);
            studioItem = await _studioItemRepository.UpdateAsync(studioItem);
            return _mapper.Map<GetStudioItemDto>(studioItem);
        }

        public async Task DeleteStudioItemAsync(int id)
        {
            var studioItem = await _studioItemRepository.DeleteAsync(id);
            if (studioItem == null)
            {
                throw new KeyNotFoundException("Studio item not found or already deleted.");
            }
        }

        public async Task<List<StudioItemTypeDto>> GetAllStudioItemTypesAsync()
        {
            var itemTypes = await _studioItemRepository.GetAllStudioItemTypesAsync();
            if (itemTypes == null || itemTypes.Count == 0)
            {
                throw new KeyNotFoundException("No item types found.");
            }
            return _mapper.Map<List<StudioItemTypeDto>>(itemTypes);
        }
    }
}
