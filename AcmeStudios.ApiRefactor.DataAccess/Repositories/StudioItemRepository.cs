using AcmeStudios.ApiRefactor.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.DataAccess.Repositories
{
    internal sealed class StudioItemRepository : IStudioItemRepository
    {
        private readonly AcmeStudiosContext _dbContext;

        private IQueryable<StudioItem> Query => _dbContext.StudioItems.Include(item => item.StudioItemType);

        public StudioItemRepository(AcmeStudiosContext dbContext)
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

        public async Task<bool> UpdateAsync(StudioItem itemToUpdate)
        {
            bool exists = await _dbContext.StudioItems.AnyAsync(x => x.StudioItemId == itemToUpdate.StudioItemId);

            if (!exists)
            {
                return false;
            }

            _dbContext.StudioItems.Update(itemToUpdate);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var entryToDelete = await _dbContext.StudioItems.FirstOrDefaultAsync(e => e.StudioItemId == id);
            
            if (entryToDelete is null)
            {
                return false;
            }

            _dbContext.StudioItems.Remove(entryToDelete);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
