using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Repository
{
    public interface IStudioRepository
    {
        public Task<ServiceResponse<ICollection<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems();
        public Task<ServiceResponse<ICollection<StudioItemType>>> GetAllStudioItemTypes();
        public Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id);
        public Task<ServiceResponse<ICollection<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem);
        public Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem);
        public Task<ServiceResponse<ICollection<GetStudioItemDto>>> DeleteStudioItem(int id);
    }
}