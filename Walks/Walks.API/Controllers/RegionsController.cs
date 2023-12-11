using Microsoft.AspNetCore.Mvc;
using Walks.API.Data;
using Walks.API.Models;
using Walks.API.Models.DTO;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext _context;
        public RegionsController(WalksDbContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            var regions = _context.Regions.ToList();
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
        public IActionResult Create(Region region)
        {
            _context.Regions.Add(region);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Read([FromRoute] Guid id) 
        {
            var region = _context.Regions.Find(id);
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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO regionDTO) 
        {
            var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if(region == null) { return NotFound(); }

            region.Name = regionDTO.Name;
            region.Code = regionDTO.Code;
            region.ImageUrl = regionDTO.ImageUrl;

            _context.SaveChanges();

            return Ok(region);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null) { return NotFound(); }

            _context.Regions.Remove(region);
            _context.SaveChanges();
            return Ok(region);
        }
    }
}
