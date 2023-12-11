using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(WalksDbContext context, IRegionRepository regionRepository) {
            _context = context;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var regions = await _regionRepository.List();
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
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Read([FromRoute] Guid id) 
        {
            var region = await _context.Regions.FindAsync(id);
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
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(region == null) { return NotFound(); }

            region.Name = regionDTO.Name;
            region.Code = regionDTO.Code;
            region.ImageUrl = regionDTO.ImageUrl;

            await _context.SaveChangesAsync();

            return Ok(region);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null) { return NotFound(); }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return Ok(region);
        }
    }
}
