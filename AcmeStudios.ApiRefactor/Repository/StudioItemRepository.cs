using AcemStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.Repository
{
    public class StudioItemRepository : IStudioItemRepository
    {
        private readonly ApplicationDbContext _context;

        public StudioItemRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<StudioItem> AddAsync(StudioItem item)
        {
            await _context.StudioItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<StudioItem>> GetAllAsync()
        {
            return await _context.StudioItems.ToListAsync();
        }

        public async Task<StudioItem> GetByIdAsync(int id)
        {
            return await _context.StudioItems
                .Include(item => item.StudioItemType) 
                .FirstOrDefaultAsync(item => item.StudioItemId == id);
        }

        public async Task<StudioItem> UpdateAsync(StudioItem item)
        {
            _context.StudioItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<StudioItem> DeleteAsync(int id)
        {
            var item = await _context.StudioItems.FindAsync(id);
            if (item != null)
            {
                _context.StudioItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<List<StudioItemType>> GetAllStudioItemTypesAsync()
        {
            return await _context.StudioItemTypes.ToListAsync();
        }
    }

}
