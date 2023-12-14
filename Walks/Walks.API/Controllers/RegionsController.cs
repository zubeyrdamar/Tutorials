using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Walks.API.CustomActionFilters;
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
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger) {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> List()
        {
            throw new Exception("This is an exception");
            _logger.LogInformation("Listing Regions");

            var regions = await _regionRepository.ListAsync();
            var regionsDTO = _mapper.Map<List<RegionDTO>>(regions);

            _logger.LogInformation("Listed Regions");

            return Ok(regionsDTO);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(CreateRegionDTO regionDTO)
        {
            var region = _mapper.Map<Region>(regionDTO);
            region = await _regionRepository.CreateAsync(region);
            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Read([FromRoute] Guid id) 
        {
            var region = await _regionRepository.ReadAsync(id);
            if(region == null) { return NotFound(); }
            var regionDTO = _mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO regionDTO) 
        {
            var region = _mapper.Map<Region>(regionDTO);
            region = await _regionRepository.UpdateAsync(id, region);
            if(region == null) { return NotFound(); }
            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if(region == null) { return NotFound(); }
            var regionDTO = _mapper.Map<Region>(region);
            return Ok(regionDTO);
        }
    }
}
