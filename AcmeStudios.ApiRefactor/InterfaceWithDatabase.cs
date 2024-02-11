using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Contracts;
using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Responses;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AcemStudios.ApiRefactor
{
    public class InterfaceWithDatabase : IInterfaceWithDatabase
    {
        private readonly IRepository<StudioItem> _studioItemRepository;
        private readonly IRepository<StudioItemType> _studioItemTypeRepository;

        public InterfaceWithDatabase(IRepository<StudioItem> studioItemRepository, IRepository<StudioItemType> studioItemTypeRepository)
        {
            _studioItemRepository = studioItemRepository;
            _studioItemTypeRepository = studioItemTypeRepository;
        }

        public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
            await _studioItemRepository.AddAsync(item);
            await _studioItemRepository.SaveChangesAsync();

            var listOfStudios = _studioItemRepository.GetAll();

            return new ServiceResponse<List<GetStudioItemDto>>
            {
                Data = await listOfStudios.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                Message = $"New item added.  Id: {item.StudioItemId}",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            var listOfStudios = _studioItemRepository.GetAll();

            return new ServiceResponse<List<GetStudioItemHeaderDto>>
            {
                Data = await listOfStudios.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync(),
                Message = "Here's all the items in your studio",
                Success = true
            };
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            //var item = await _cont.StudioItems
            //.Where(item => item.StudioItemId == id)
            //.Include(type => type.StudioItemType)
            //.FirstOrDefaultAsync();

            var studio = await _studioItemRepository.GetByIdAsync(e => e.StudioItemId == id);

            return new ServiceResponse<GetStudioItemDto>
            {
                Data = _mapper.Map<GetStudioItemDto>(studio),
                Message = "Here's your selected studio item",
                Success = true
            };
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            StudioItem studioItem = await _studioItemRepository.GetByIdAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);

            if (studioItem is null)
            {
                return new ServiceResponse<GetStudioItemDto>
                {
                    Data = null,
                    Message = $"Studio Item {updatedStudioItem.StudioItemId} not found",
                    Success = false
                };
            }

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

                serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItem);
                serviceResponse.Message = "Update successful";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            _studioItemRepository.Update(studioItem);
            await _studioItemRepository.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

            try
            {
                StudioItem studioItem = await _studioItemRepository.GetByIdAsync(c => c.StudioItemId == id);

                if (studioItem is null)
                {
                    return new ServiceResponse<List<GetStudioItemDto>>
                    {
                        Data = null,
                        Message = $"Studio Item {id} not found",
                        Success = false
                    };
                }

                _studioItemRepository.Delete(studioItem);
                await _studioItemRepository.SaveChangesAsync();

                var listOfStudios = _studioItemRepository.GetAll();

                serviceResponse.Data = await listOfStudios.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();
                serviceResponse.Message = "Item deleted";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var _mapper = config.CreateMapper();

            return new ServiceResponse<List<StudioItemType>>
            {
                Data = await _studioItemTypeRepository.GetAll().OrderBy(s => s.Value).ToListAsync(),
                Message = "Item types fetched",
                Success = true
            };
        }
    }
}
