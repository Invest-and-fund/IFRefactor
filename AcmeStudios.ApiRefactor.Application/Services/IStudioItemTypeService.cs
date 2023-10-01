using AcmeStudios.ApiRefactor.Application.DTOs;

namespace AcmeStudios.ApiRefactor.Application.Services
{
    public interface IStudioItemTypeService
    {
        Task<ServiceResponse<IEnumerable<StudioItemTypeDto>>> GetAllStudioItemTypesAsync();
    }
}
