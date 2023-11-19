using AcmeStudios.ApiRefactor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Repository
{
    public interface IStudioItemRepository
    {
        Task<StudioItem> AddAsync(StudioItem item);
        Task<List<StudioItem>> GetAllAsync();
        Task<StudioItem> GetByIdAsync(int id);
        Task<StudioItem> UpdateAsync(StudioItem item);
        Task<StudioItem> DeleteAsync(int id);
        Task<List<StudioItemType>> GetAllStudioItemTypesAsync();

    }

}
