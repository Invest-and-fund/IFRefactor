using System.Collections.Generic;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Responses;

namespace AcmeStudios.ApiRefactor.Contracts
{
    public interface IInterfaceWithDatabase
    {
        Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem);
        Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id);
        Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems();
        Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes();
        Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id);
        Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem);
    }
}