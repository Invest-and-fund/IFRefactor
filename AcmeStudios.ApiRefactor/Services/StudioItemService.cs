using AcemStudios.ApiRefactor;
using AcemStudios.ApiRefactor.Data;
using AcemStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.DomainModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor
{
    public class StudioItemService : IStudioItemService
    {
        private readonly Cont m_studioItemContext;

        public StudioItemService(Cont context)
        {
            m_studioItemContext = context;
        }

        public async Task<List<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
            await m_studioItemContext.StudioItems.AddAsync(item);
            await m_studioItemContext.SaveChangesAsync();

            var addedItems = await m_studioItemContext.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();

            return addedItems;
        }

        public async Task<List<GetStudioItemDto>> DeleteStudioItemAsync(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();
            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

            try
            {
                StudioItem item = await m_studioItemContext.StudioItems.FirstAsync(c => c.StudioItemId == id);
                m_studioItemContext.Remove(item);
                await m_studioItemContext.SaveChangesAsync();

                return await m_studioItemContext.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ItemDeleteException("", ex);
            }
        }

        public async Task<List<GetStudioItemHeaderDto>> GetAllStudioHeaderItemsAsync()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            return await m_studioItemContext.StudioItems.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync();
        }

        public async Task<List<StudioItemType>> GetAllStudioItemTypesAsync()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            return await m_studioItemContext.StudioItemTypes.OrderBy(s => s.Value).ToListAsync();
        }

        public async Task<GetStudioItemDto> GetStudioItemByIdAsync(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();
            var item = await m_studioItemContext.StudioItems
            .Where(item => item.StudioItemId == id)
            .Include(type => type.StudioItemType)
            .FirstOrDefaultAsync();

            return _mapper.Map<GetStudioItemDto>(item);
        }

        public async Task<GetStudioItemDto> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            StudioItem studioItem = await m_studioItemContext.StudioItems
                .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);
            var _mapper = config.CreateMapper();
            try
            {
                studioItem.Acquired = updatedStudioItem.Acquired;
                studioItem.Description = updatedStudioItem.Description;
                studioItem.Eurorack = updatedStudioItem.Eurorack;
                studioItem.Name = updatedStudioItem.Name;
                studioItem.Price = updatedStudioItem.Price;
                studioItem.SerialNumber = updatedStudioItem.SerialNumber;
                studioItem.Sold = updatedStudioItem.Sold;
                studioItem.SoldFor = updatedStudioItem.SoldFor;
                studioItem.StudioItemType = updatedStudioItem.StudioItemType;

            }
            catch (Exception ex)
            {
                throw new ItemUpdateException("", ex);
            }

            m_studioItemContext.StudioItems.Update(studioItem);
            await m_studioItemContext.SaveChangesAsync();

            return _mapper.Map<GetStudioItemDto>(studioItem);
        }
    }
}
