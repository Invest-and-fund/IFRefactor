using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var data = await GetAllStudioItemDtos();

            return CreateServiceResponse(data, $"New item added.  Id: {item.StudioItemId}", true);
        }

        public async Task<ServiceResponse<List<StudioItemHeaderDto>>> GetAllStudioHeaderItems()
        {
            var listOfStudios = _studioItemRepository.GetAll();
            var data = await listOfStudios.Select(s => _mapper.Map<StudioItemHeaderDto>(s)).ToListAsync();

            return CreateServiceResponse(data, "Here's all the items in your studio", true);
        }

        public async Task<ServiceResponse<StudioItemDto>> GetStudioItemById(int id)
        {
            var studioItem = await _studioItemRepository.GetByIdAsync(e => e.StudioItemId == id, includes: e => e.StudioItemType);

            if (studioItem is null)
            {
                return CreateServiceResponse<StudioItemDto>(null, $"Studio Item {id} not found", false);
            }

            var data = _mapper.Map<StudioItemDto>(studioItem);

            return CreateServiceResponse(data, "Here's your selected studio item", true);
        }

        public async Task<ServiceResponse<StudioItemDto>> UpdateStudioItem(StudioItemForUpdateDto updatedStudioItem)
        {
            var serviceResponse = new ServiceResponse<StudioItemDto>();

            StudioItem studioItem = await _studioItemRepository.GetByIdAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId, includes: e => e.StudioItemType);

            if (studioItem is null)
            {
                return CreateServiceResponse<StudioItemDto>(null, $"Studio Item {updatedStudioItem.StudioItemId} not found", false);
            }

            try
            {
                studioItem = _mapper.Map<StudioItem>(updatedStudioItem);

                _studioItemRepository.Update(studioItem);
                await _studioItemRepository.SaveChangesAsync();

                var data = _mapper.Map<StudioItemDto>(studioItem);

                serviceResponse = CreateServiceResponse(data, "Update successful", true);
            }
            catch (Exception ex)
            {
                serviceResponse = CreateServiceResponse<StudioItemDto>(null, ex.Message, false);
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
                    return CreateServiceResponse<List<StudioItemDto>>(null, $"Studio Item {id} not found", false);
                }

                _studioItemRepository.Delete(studioItem);
                await _studioItemRepository.SaveChangesAsync();

                var data = await GetAllStudioItemDtos();

                serviceResponse = CreateServiceResponse(data, "Item deleted", true);
            }
            catch (Exception ex)
            {
                serviceResponse = CreateServiceResponse<List<StudioItemDto>>(null, ex.Message, false);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudioItemTypeDto>>> GetAllStudioItemTypes()
        {
            var listOfStuidoItemTypes = _studioItemTypeRepository.GetAll();
            var data = await listOfStuidoItemTypes.OrderBy(s => s.Value).Select(s => _mapper.Map<StudioItemTypeDto>(s)).ToListAsync();

            return CreateServiceResponse(data, "Item types fetched", true);
        }

        private async Task<List<StudioItemDto>> GetAllStudioItemDtos()
        {
            var listOfStudios = _studioItemRepository.GetAll(includes: e => e.StudioItemType);

            return await listOfStudios.Select(c => _mapper.Map<StudioItemDto>(c)).ToListAsync();
        }

        private static ServiceResponse<T> CreateServiceResponse<T>(T data, string message, bool isSuccess) where T : class
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Message = message,
                Success = isSuccess
            };
        }
    }
}
