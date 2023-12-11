using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository repository;

        public WalksController(IMapper mapper, IWalkRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] string filterOn, [FromQuery] string filterQuery,
            [FromQuery] string sortBy, [FromQuery] bool isAscending
        ) 
        {
            var walks = await repository.ListAsync(filterOn, filterQuery, sortBy, isAscending);
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDTO walkDTO)
        {
            var walk = mapper.Map<Walk>(walkDTO);
            await repository.CreateAsync(walk);
            return Ok(mapper.Map<CreateWalkDTO>(walk));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Read([FromRoute] Guid id)
        {
            var walk = await repository.ReadAsync(id);
            if (walk == null) { return NotFound(); }
            return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDTO walkDTO)
        {
            var walk = mapper.Map<Walk>(walkDTO);
            walk = await repository.UpdateAsync(id, walk);
            if(walk == null) { return NotFound(); }
            return Ok(mapper.Map<UpdateWalkDTO>(walk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await repository.DeleteAsync(id);
            if(walk == null) { return NotFound(); }
            return Ok("Deleted successfully");
        }
    }
}
