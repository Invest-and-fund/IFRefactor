using AcemStudios.ApiRefactor.Data;
using AcemStudios.ApiRefactor.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcemStudios.ApiRefactor
{
        public class InterfaceWithDatabase
        {
            private readonly IConfiguration _configuration;
            private readonly Cont _cont;
            private readonly IMapper _mapper;

            // Constructor that injects IConfiguration, Cont, and IMapper
            public InterfaceWithDatabase(IConfiguration configuration, Cont cont, IMapper mapper)
            {
                _configuration = configuration;
                _cont = cont;
                _mapper = mapper;
            }

            // Refactor 1: Combined common exception handling logic into a private method
            private ServiceResponse<T> HandleException<T>(Exception ex, string message)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = message + ": " + ex.Message
                };
            }

            // Refactor 2: Added comments to explain the purpose of each method

            // Add a new studio item
            public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
            {
                try
                {
                    // Map DTO to entity
                    StudioItem item = _mapper.Map<StudioItem>(newStudioItem);

                    // Add to the database and save changes
                    await _cont.StudioItems.AddAsync(item);
                    await _cont.SaveChangesAsync();

                    var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
                    {
                        Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                        Message = $"New item added.  Id: {item.StudioItemId}",
                        Success = true
                    };

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<List<GetStudioItemDto>>(ex, "Error adding studio item");
                }
            }

            // Get all studio header items
            public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
            {
                try
                {
                    var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
                    {
                        Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync(),
                        Message = "Here's all the items in your studio",
                        Success = true
                    };

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<List<GetStudioItemHeaderDto>>(ex, "Error getting studio header items");
                }
            }

            // Get a studio item by ID
            public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
            {
                try
                {
                    var item = await _cont.StudioItems
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
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<GetStudioItemDto>(ex, "Error getting studio item by ID");
                }
            }

            // Update a studio item
            public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
            {
                try
                {
                    var studioItem = await _cont.StudioItems
                        .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);

                    // Map updated data to the entity
                    _mapper.Map(updatedStudioItem, studioItem);

                    _cont.StudioItems.Update(studioItem);
                    await _cont.SaveChangesAsync();

                    var serviceResponse = new ServiceResponse<GetStudioItemDto>
                    {
                        Data = _mapper.Map<GetStudioItemDto>(studioItem),
                        Message = "Update successful",
                        Success = true
                    };

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<GetStudioItemDto>(ex, "Error updating studio item");
                }
            }

            // Delete a studio item by ID
            public async Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id)
            {
                try
                {
                    var item = await _cont.StudioItems.FirstOrDefaultAsync(c => c.StudioItemId == id);
                    if (item != null)
                    {
                        _cont.Remove(item);
                        await _cont.SaveChangesAsync();
                    }

                    var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
                    {
                        Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                        Success = true,
                        Message = "Item deleted"
                    };

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<List<GetStudioItemDto>>(ex, "Error deleting studio item");
                }
            }

            // Get all studio item types
            public async Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes()
            {
                try
                {
                    var serviceResponse = new ServiceResponse<List<StudioItemType>>
                    {
                        Data = await _cont.StudioItemTypes.OrderBy(s => s.Value).ToListAsync(),
                        Message = "Item types fetched",
                        Success = true
                    };

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return an error response
                    return HandleException<List<StudioItemType>>(ex, "Error getting studio item types");
                }
            }
        }
    
    public class StudioItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemId { get; set; }
        public DateTime Acquired { get; set; }
        public DateTime? Sold { get; set; } = null;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public decimal Price { get; set; } //= 10.00M;
        public decimal? SoldFor { get; set; } //= 0M;
        public bool Eurorack { get; set; } //= false;
        [Required]
        public int StudioItemTypeId { get; set; }
        public StudioItemType StudioItemType { get; set; }


    }

    public class StudioItemType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemTypeId { get; set; }
        [Required]
        public string Value { get; set; }
        [JsonIgnore]
        public ICollection<StudioItem> StudioItem { get; set; }
    }

    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }

}
