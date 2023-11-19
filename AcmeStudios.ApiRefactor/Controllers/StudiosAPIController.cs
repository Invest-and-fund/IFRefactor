using AcmeStudios.ApiRefactor.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Models.DTOs;
using AcmeStudios.ApiRefactor.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AcmeStudios.ApiRefactor.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiosApiController : ControllerBase
    {
        public APIResponse _response;
        private readonly IStudioItemService _studioItemService;
        private readonly IMapper _mapper;

        public StudiosApiController(IStudioItemService studioItemService, IMapper mapper)
        {
            _studioItemService = studioItemService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllStudioItems()
        {
            try
            {
                var studioItems = await _studioItemService.GetAllStudioItemsAsync();
                _response.Result = _mapper.Map<List<GetStudioItemDto>>(studioItems);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpGet("{id:int}", Name = "GetStudioItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudioItemById(int id)
        {
            try
            {
                var studioItem = await _studioItemService.GetStudioItemByIdAsync(id);
                _response.Result = _mapper.Map<GetStudioItemDto>(studioItem);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (KeyNotFoundException)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddStudioItem([FromBody] AddStudioItemDto studioItemDto)
        {
            try
            {
                if (studioItemDto == null)
                {
                    return BadRequest();
                }
                var newStudioItem = await _studioItemService.AddStudioItemAsync(studioItemDto);
                _response.Result = _mapper.Map<GetStudioItemDto>(newStudioItem);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetStudioItem", new { id = newStudioItem.StudioItemId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateStudioItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateStudioItem(int id, [FromBody] UpdateStudioItemDto studioItemDto)
        {
            try
            {
                if (studioItemDto == null || id != studioItemDto.StudioItemId)
                {
                    return BadRequest();
                }
                var updatedStudioItem = await _studioItemService.UpdateStudioItemAsync(studioItemDto);
                _response.Result = _mapper.Map<GetStudioItemDto>(updatedStudioItem);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (KeyNotFoundException)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteStudioItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteStudioItem(int id)
        {
            try
            {
                await _studioItemService.DeleteStudioItemAsync(id);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (KeyNotFoundException)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudioItemTypes()
        {
            try
            {
                var studioItemTypesDto = await _studioItemService.GetAllStudioItemTypesAsync();
                _response.Result = studioItemTypesDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.StatusCode, _response);
            }
        }
    }
}
