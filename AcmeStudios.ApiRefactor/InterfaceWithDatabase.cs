using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DataAccess;
using AcmeStudios.ApiRefactor.Domain;
using AcmeStudios.ApiRefactor.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor
{
    public class InterfaceWithDatabase
    {
        private readonly Cont _dbContext;
        private readonly IMapper _mapper;

        public InterfaceWithDatabase(Cont dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
            await _dbContext.StudioItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
            {
                Data = await _dbContext.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                Message = $"New item added.  Id: {item.StudioItemId}",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioItemHeaders()
        {
            var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
            {
                Data = await _dbContext.StudioItems.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync(),
                Message = "Here's all the items in your studio",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
        {
            var item = await _dbContext.StudioItems
            .Where(item => item.StudioItemId == id)
            .Include(type => type.StudioItemType)
            .FirstOrDefaultAsync();

            var serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = _mapper.Map<GetStudioItemDto>(item),
                Message = "Here's your selected studio item",
                Success = true
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
        {
            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            StudioItem studioItem = await _dbContext.StudioItems
                .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);
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
                studioItem.StudioItemType = _mapper.Map<StudioItemType>(updatedStudioItem.StudioItemType);  // TODO this probably doesn't work

                serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItem);
                serviceResponse.Message = "Update successful";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            _dbContext.StudioItems.Update(studioItem);
            await _dbContext.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

            try
            {
                StudioItem item = await _dbContext.StudioItems.FirstAsync(c => c.StudioItemId == id);
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync();

                serviceResponse.Data = await _dbContext.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();
                serviceResponse.Success = true;
                serviceResponse.Message = "Item deleted";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes()
        {
            var serviceResponse = new ServiceResponse<List<StudioItemType>>
            {
                Data = await _dbContext.StudioItemTypes.OrderBy(s => s.Value).ToListAsync(),
                Message = "Item types fetched",
                Success = true
            };

            return serviceResponse;
        }
    }

    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }

}
