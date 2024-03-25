using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Interfaces;
using AcmeStudios.ApiRefactor.Repositories;
using AcmeStudios.ApiRefactor.Response;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AcmeStudios.ApiRefactor.Services;

 public class StudioItemServices(IConfiguration configuration,
     IRepository<StudioItem> studioItemRepository,
     IRepository<StudioItemType>  studioItemTypeRepository, 
     IMapper mapper)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
    {
        var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();
        try
        {
            var item = mapper.Map<StudioItem>(newStudioItem);
            await studioItemRepository.AddAsync(item);
            await studioItemRepository.SaveChangesAsync();
            var itemList = await studioItemRepository.GetAll();
            var mappedItemList = itemList.Select(item => mapper.Map<GetStudioItemDto>(item)).ToList();
            
            serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
            {
                Data = mappedItemList,
                Message = $"New item added. Id: {item.Id}",
                Success = true
            };

            return serviceResponse;
        }
        catch (Exception ex)
        {
            return serviceResponse.HandleError<List<GetStudioItemDto>>(ex.Message);
        }
    }

    public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
    {
        var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>();
        try
        {
            var itemList = await studioItemRepository.GetAll();
            var mappedItemList = itemList.Select(item => mapper.Map<GetStudioItemHeaderDto>(item)).ToList();

            serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
            {
                Data = mappedItemList,
                Message = "Here's all the items in your studio",
                Success = true
            };

            return serviceResponse;
        }
        catch (Exception ex)
        {
            return serviceResponse.HandleError<List<GetStudioItemHeaderDto>>(ex.Message);
        }
    }

    public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
    {
        var serviceResponse = new ServiceResponse<GetStudioItemDto>();
        try
        {
            var item = await studioItemRepository.Get(id);

            if (item == null)
            {
                return new ServiceResponse<GetStudioItemDto>
                {
                    Success = false,
                    Message = $"Studio item with ID {id} not found"
                };
            }

            serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = mapper.Map<GetStudioItemDto>(item),
                Message = "Here's your selected studio item",
                Success = true
            };
            return serviceResponse;
        }
        catch (Exception ex)
        {
            return serviceResponse.HandleError<GetStudioItemDto>(ex.Message);
        }
    }

    public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
    {
        var serviceResponse = new ServiceResponse<GetStudioItemDto>();
        try
        {
            var studioItem = await studioItemRepository.FirstOrDefaultAsync(c => c.Id == updatedStudioItem.StudioItemId);

            if (studioItem == null)
            {
                return new ServiceResponse<GetStudioItemDto>
                {
                    Success = false,
                    Message = $"Studio item with ID {updatedStudioItem.StudioItemId} not found"
                };
            }

            studioItem.Acquired = updatedStudioItem.Acquired;
            studioItem.Description = updatedStudioItem.Description;
            studioItem.Eurorack = updatedStudioItem.Eurorack;
            studioItem.Name = updatedStudioItem.Name;
            studioItem.Price = updatedStudioItem.Price;
            studioItem.SerialNumber = updatedStudioItem.SerialNumber;
            studioItem.Sold = updatedStudioItem.Sold;
            studioItem.SoldFor = updatedStudioItem.SoldFor;
            studioItem.StudioItemType = updatedStudioItem.StudioItemType;

            await studioItemRepository.Update(studioItem);
            await studioItemRepository.SaveChangesAsync();

            serviceResponse = new ServiceResponse<GetStudioItemDto>
            {
                Data = mapper.Map<GetStudioItemDto>(studioItem),
                Message = "Update successful",
                Success = true
            };

            return serviceResponse;
        }
        catch (Exception ex)
        {
            return serviceResponse.HandleError<GetStudioItemDto>(ex.Message);
        }
    }

    public async Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(long id)
    {
            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

            try
            {
                var item = await studioItemRepository.FirstAsync(c => c.Id == id);
                await studioItemRepository.Delete(item.Id);
                await studioItemRepository.SaveChangesAsync();

                serviceResponse.Data = await studioItemRepository.Select(c => mapper.Map<GetStudioItemDto>(c));
                serviceResponse.Success = true;
                serviceResponse.Message = "Item deleted";
            }
            catch (Exception ex)
            {
                return serviceResponse.HandleError<List<GetStudioItemDto>>(ex.Message);
            }

            return serviceResponse;
        
    }
    
    public async Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes()
    {
        var serviceResponse = new ServiceResponse<List<StudioItemType>>();
        try
        {
            var data = await studioItemTypeRepository
                .Select(s => s, orderBy: s => s.Value);
            serviceResponse = new ServiceResponse<List<StudioItemType>>
            {
                Data = data,
                Message = "Item types fetched",
                Success = true
            };
        }
        catch (Exception ex)
        {
            return serviceResponse.HandleError<List<StudioItemType>>(ex.Message);
        }

        return serviceResponse;
    }
}