using System.Collections.Generic;
using System.Threading.Tasks;

using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Responses;

namespace AcmeStudios.ApiRefactor.Contracts
{
    public interface IStudioHandler
    {
        Task<ServiceResponse<List<StudioItemDto>>> AddStudioItem(StudioItemForCreationDto newStudioItem);
        Task<ServiceResponse<List<StudioItemDto>>> DeleteStudioItem(int id);
        Task<ServiceResponse<List<StudioItemHeaderDto>>> GetAllStudioHeaderItems();
        Task<ServiceResponse<List<StudioItemTypeDto>>> GetAllStudioItemTypes();
        Task<ServiceResponse<StudioItemDto>> GetStudioItemById(int id);
        Task<ServiceResponse<StudioItemDto>> UpdateStudioItem(StudioItemForUpdateDto updateStudioItem);
    }
}