using AcmeStudios.ApiRefactor.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.DataAccess.Repositories
{
    internal sealed class StudioItemRepository : IStudioItemRepository
    {
        private readonly Cont _dbContext;

        private IQueryable<StudioItem> Query => _dbContext.StudioItems.Include(item => item.StudioItemType);

        public StudioItemRepository(Cont dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(StudioItem itemToAdd)
        {
            _dbContext.StudioItems.Add(itemToAdd);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StudioItem>> GetAllAsync()
        {
            return await Query.ToListAsync();
        }

        public async Task<StudioItem?> GetByIdAsync(int id)
        {
            return await _dbContext.StudioItems
                .Where(item => item.StudioItemId == id)
                .Include(type => type.StudioItemType)
                .FirstOrDefaultAsync();
        }

        // TODO test updating an item that doesn't exist
        public async Task<bool> UpdateAsync(StudioItem itemToUpdate)
        {
            _dbContext.StudioItems.Update(itemToUpdate);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        // TODO test deleting an item that doesn't exist
        public async Task<bool> RemoveAsync(int id)
        {
            var entryToDelete = await _dbContext.StudioItems.FirstOrDefaultAsync(e => e.StudioItemId == id);
            var successful = false;

            if (entryToDelete is not null)
            {
                _dbContext.StudioItems.Remove(entryToDelete);
                successful = await _dbContext.SaveChangesAsync() > 0;
            }

            return successful;
        }
    }
}
