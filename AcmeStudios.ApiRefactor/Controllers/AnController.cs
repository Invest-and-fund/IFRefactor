using AcemStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AcmeStudios.ApiRefactor.DomainModels;
using System;

namespace AcemStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class AnController : ControllerBase
    {
        private readonly IStudioItemService m_studioItemService;

        public AnController(IStudioItemService studioItemService)
        {
            m_studioItemService = studioItemService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var studioHeaderItems = await m_studioItemService.GetAllStudioHeaderItemsAsync();
                var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
                {
                    Data = studioHeaderItems,
                    Message = "Here's all the items in your studio",
                    Success = true
                };

                return Ok(serviceResponse);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var studioItem = await m_studioItemService.GetStudioItemByIdAsync(id);
                var serviceResponse = new ServiceResponse<GetStudioItemDto>
                {
                    Data = studioItem,
                    Message = "Here's your selected studio item",
                    Success = true
                };
                return Ok(serviceResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            try
            {
                var addedStudioItems = await m_studioItemService.AddStudioItemAsync(studioItem);

                var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
                {
                    Data = addedStudioItems,
                    Message = $"New item added.  Id: {addedStudioItems.First().StudioItemId}",
                    Success = true
                };

                return Ok(serviceResponse);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            try
            {
                var updatedItem = await m_studioItemService.UpdateStudioItemAsync(studioItem);

                serviceResponse.Data = updatedItem;
                serviceResponse.Message = "Update successful";
                serviceResponse.Success = true;
            }
            catch (ItemUpdateException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

            try
            {
                var deletedItem = await m_studioItemService.DeleteStudioItemAsync(id);
                serviceResponse.Data = deletedItem;
                serviceResponse.Success = true;
                serviceResponse.Message = "Item deleted";
            }
            catch (ItemDeleteException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(serviceResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            try
            {
                var studioTypes = await m_studioItemService.GetAllStudioItemTypesAsync();
                var serviceResponse = new ServiceResponse<List<StudioItemType>>
                {
                    Data = studioTypes,
                    Message = "Item types fetched",
                    Success = true
                };

                return Ok(serviceResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}