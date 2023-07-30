using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioRepository _studioRepository;

        public StudioController(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        [HttpGet("StudioHeaderItems")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetStudioItemHeaderDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            var result = await _studioRepository.GetAllStudioHeaderItems();
            
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("StudioItemTypes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudioItemType>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            var result = await _studioRepository.GetAllStudioItemTypes();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("StudioItem/{id}")]
        [ProducesResponseType(200, Type = typeof(GetStudioItemDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studioRepository.GetStudioItemById(id);
            
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("StudioItem")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetStudioItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            var result = await _studioRepository.AddStudioItem(studioItem);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("StudioItem")]
        [ProducesResponseType(200, Type = typeof(GetStudioItemDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            var result = await _studioRepository.UpdateStudioItem(studioItem);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("StudioItem/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetStudioItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studioRepository.DeleteStudioItem(id);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}