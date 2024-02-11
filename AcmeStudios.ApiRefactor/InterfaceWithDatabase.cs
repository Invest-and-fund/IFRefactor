using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Contracts;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Responses;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AcemStudios.ApiRefactor
{
    public class InterfaceWithDatabase : IInterfaceWithDatabase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StudioItem> _studioItemRepository;
        private readonly IRepository<StudioItemType> _studioItemTypeRepository;

        public InterfaceWithDatabase(IMapper mapper, IRepository<StudioItem> studioItemRepository, IRepository<StudioItemType> studioItemTypeRepository)
        {
            _mapper = mapper;
            _studioItemRepository = studioItemRepository;
            _studioItemTypeRepository = studioItemTypeRepository;
        }

        public async Task<ServiceResponse<List<StudioItemDto>>> AddStudioItem(StudioItemForCreationDto newStudioItem)
        {
            StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
            await _studioItemRepository.AddAsync(item);
            await _studioItemRepository.SaveChangesAsync();

            return new ServiceResponse<List<StudioItemDto>>
            {
                Data = await GetAllStudioItemDtos(),
                Message = $"New item added.  Id: {item.StudioItemId}",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<StudioItemHeaderDto>>> GetAllStudioHeaderItems()
        {
            var listOfStudios = _studioItemRepository.GetAll();

            return new ServiceResponse<List<StudioItemHeaderDto>>
            {
                Data = await listOfStudios.Select(s => _mapper.Map<StudioItemHeaderDto>(s)).ToListAsync(),
                Message = "Here's all the items in your studio",
                Success = true
            };
        }

        public async Task<ServiceResponse<StudioItemDto>> GetStudioItemById(int id)
        {
            var studio = await _studioItemRepository.GetByIdAsync(e => e.StudioItemId == id, includes: e => e.StudioItemType);

            return new ServiceResponse<StudioItemDto>
            {
                Data = _mapper.Map<StudioItemDto>(studio),
                Message = "Here's your selected studio item",
                Success = true
            };
        }

        public async Task<ServiceResponse<StudioItemDto>> UpdateStudioItem(StudioItemForUpdateDto updatedStudioItem)
        {
            var serviceResponse = new ServiceResponse<StudioItemDto>();

            StudioItem studioItem = await _studioItemRepository.GetByIdAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);

            if (studioItem is null)
            {
                return new ServiceResponse<StudioItemDto>
                {
                    Data = null,
                    Message = $"Studio Item {updatedStudioItem.StudioItemId} not found",
                    Success = false
                };
            }

            try
            {
                studioItem = _mapper.Map<StudioItem>(updatedStudioItem);

                _studioItemRepository.Update(studioItem);
                await _studioItemRepository.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<StudioItemDto>(studioItem);
                serviceResponse.Message = "Update successful";
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

        public async Task<ServiceResponse<List<StudioItemDto>>> DeleteStudioItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<StudioItemDto>>();

            try
            {
                StudioItem studioItem = await _studioItemRepository.GetByIdAsync(c => c.StudioItemId == id);

                if (studioItem is null)
                {
                    return new ServiceResponse<List<StudioItemDto>>
                    {
                        Data = null,
                        Message = $"Studio Item {id} not found",
                        Success = false
                    };
                }

                _studioItemRepository.Delete(studioItem);
                await _studioItemRepository.SaveChangesAsync();

                serviceResponse.Data = await GetAllStudioItemDtos();
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

        public async Task<ServiceResponse<List<StudioItemTypeDto>>> GetAllStudioItemTypes()
        {
            var listOfStuidoItemTypes = _studioItemTypeRepository.GetAll();

            return new ServiceResponse<List<StudioItemTypeDto>>
            {
                Data = await listOfStuidoItemTypes.OrderBy(s => s.Value).Select(s => _mapper.Map<StudioItemTypeDto>(s)).ToListAsync(),
                Message = "Item types fetched",
                Success = true
            };
        }

        private async Task<List<StudioItemDto>> GetAllStudioItemDtos()
        {
            var listOfStudios = _studioItemRepository.GetAll(includes: e => e.StudioItemType);

            return await listOfStudios.Select(c => _mapper.Map<StudioItemDto>(c)).ToListAsync();
        }
    }
}
