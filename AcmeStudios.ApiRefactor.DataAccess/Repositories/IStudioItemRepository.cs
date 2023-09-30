using AcmeStudios.ApiRefactor.Domain;

namespace AcmeStudios.ApiRefactor.DataAccess.Repositories
{
    public interface IStudioItemRepository
    {
        public Task<IEnumerable<StudioItem>> GetAllAsync();
        public Task<StudioItem?> GetByIdAsync(int id);
        public Task<bool> AddAsync(StudioItem itemToAdd);
        public Task<bool> UpdateAsync(StudioItem itemToUpdate);
        public Task<bool> RemoveAsync(int id);
    }
}
