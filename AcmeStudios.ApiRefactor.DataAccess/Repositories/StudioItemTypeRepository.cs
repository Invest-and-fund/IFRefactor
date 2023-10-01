using AcmeStudios.ApiRefactor.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.DataAccess.Repositories
{
    internal sealed class StudioItemTypeRepository : IStudioItemTypeRepository
    {
        private readonly AcmeStudiosContext _dbContext;

        public StudioItemTypeRepository(AcmeStudiosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StudioItemType>> GetAllAsync()
        {
            return await _dbContext.StudioItemTypes.OrderBy(s => s.Value).ToListAsync();
        }
    }
}
