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

            return ServiceResponse<GetStudioItemDto>
                .Succeeded(_mapper.Map<GetStudioItemDto>(item), $"New item added.  Id: {item.StudioItemId}");
        }

        public async Task<ServiceResponse<int>> DeleteStudioItemAsync(int id)
        {
            var success = await _studioItemRepository.RemoveAsync(id);

            return success 
                ? ServiceResponse<int>.Succeeded(id, "Item deleted") 
                : ServiceResponse<int>.Failed("An unexpected error occurred when trying to delete the studio item. Does it definitely exist?");
        }

        public async Task<ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>> GetAllStudioItemHeadersAsync()
        {
            var items = await _studioItemRepository.GetAllAsync();

            return ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>
                .Succeeded(_mapper.Map<IEnumerable<GetStudioItemHeaderDto>>(items), "Here's all the items in your studio");
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemByIdAsync(int id)
        {
            var item = await _studioItemRepository.GetByIdAsync(id);

            return ServiceResponse<GetStudioItemDto>
                .Succeeded(_mapper.Map<GetStudioItemDto>(item), "Here's your selected studio item");
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem)
        {
            var studioItem = _mapper.Map<StudioItem>(updatedStudioItem);

            var success = await _studioItemRepository.UpdateAsync(studioItem);

            return success 
                ? ServiceResponse<GetStudioItemDto>.Succeeded(_mapper.Map<GetStudioItemDto>(studioItem), "Update Successful") 
                : ServiceResponse<GetStudioItemDto>.Failed("An unexpected error occurred when trying to update the studio item. Does it definitely exist?");
        }
    }
}
