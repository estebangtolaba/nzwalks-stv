using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger) 
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            return Ok(regions.Select(x => mapper.Map<RegionDTO>(x)));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.GetByIdAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var newRegion = mapper.Map<Region>(createRegionDTO);
            var regionDomainModel = await regionRepository.CreateAsync(newRegion);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpPut]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(UpdateRegionDTO updateRegionDTO)
        {
            var updatedRegion = mapper.Map<Region>(updateRegionDTO);
            var regionDomainModel = await regionRepository.UpdateAsync(updateRegionDTO.Id, updatedRegion);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }
}
