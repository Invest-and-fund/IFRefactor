using AcmeStudios.ApiRefactor.Application.DTOs;

namespace AcmeStudios.ApiRefactor.Application.Services
{
    public interface IStudioItemService
    {
        Task<ServiceResponse<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem);
        Task<ServiceResponse<IEnumerable<GetStudioItemHeaderDto>>> GetAllStudioItemHeadersAsync();
        Task<ServiceResponse<GetStudioItemDto>> GetStudioItemByIdAsync(int id);
        Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem);
        Task<ServiceResponse<int>> DeleteStudioItemAsync(int id);
    }
}
