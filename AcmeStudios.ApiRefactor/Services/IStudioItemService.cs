using AcmeStudios.ApiRefactor.Models.DTOs;
using AcmeStudios.ApiRefactor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Models.DTOs.AcmeStudios.ApiRefactor.Models.Dto;

namespace AcmeStudios.ApiRefactor.Services
{
    public interface IStudioItemService
    {
        Task<List<GetStudioItemDto>> GetAllStudioItemsAsync();
        Task<GetStudioItemDto> GetStudioItemByIdAsync(int id);
        Task<GetStudioItemDto> AddStudioItemAsync(AddStudioItemDto newStudioItem);
        Task<GetStudioItemDto> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem);
        Task DeleteStudioItemAsync(int id);
        Task<List<StudioItemTypeDto>> GetAllStudioItemTypesAsync();

    }

}
