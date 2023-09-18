using AcemStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor
{
    public interface IStudioItemService
    {
        Task<List<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem);

        Task<List<GetStudioItemHeaderDto>> GetAllStudioHeaderItemsAsync();

        Task<GetStudioItemDto> GetStudioItemByIdAsync(int id);

        Task<GetStudioItemDto> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem);

        Task<List<GetStudioItemDto>> DeleteStudioItemAsync(int id);

        Task<List<StudioItemType>> GetAllStudioItemTypesAsync();
    }
}
