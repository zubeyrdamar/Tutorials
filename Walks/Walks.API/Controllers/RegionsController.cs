using Microsoft.AspNetCore.Mvc;
using Walks.API.Models;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository regionRepository) {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var regions = await _regionRepository.ListAsync();
            var regionsDTO = new List<RegionDTO>();
            foreach(var region in regions)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Code = region.Code,
                    Name = region.Name,
                });
            }

            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Region region)
        {
            var tempRegion = await _regionRepository.CreateAsync(region);
            return Ok(tempRegion);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Read([FromRoute] Guid id) 
        {
            var region = await _regionRepository.ReadAsync(id);
            if(region == null) { return NotFound(); }
            var regionDTO = new RegionDTO()
            {
                Code = region.Code,
                Name = region.Name,
            };
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO regionDTO) 
        {
            var region = await _regionRepository.UpdateAsync(id, regionDTO);
            if(region == null) { return NotFound(); }

            return Ok(region);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if(region == null) { return NotFound(); } 
            return Ok(region);
        }
    }
}
