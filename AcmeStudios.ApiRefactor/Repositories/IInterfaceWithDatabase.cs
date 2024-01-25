using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DTOs;

namespace AcmeStudios.ApiRefactor.Repositories;

public interface IInterfaceWithDatabase
{
    Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem);
    Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems();
    Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id);
    Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem);
    Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id);
    Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes();
}
