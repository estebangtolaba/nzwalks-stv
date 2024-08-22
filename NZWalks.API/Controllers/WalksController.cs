using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterValue, [FromQuery] string? sortBy, [FromQuery] bool? ascending, [FromQuery] int page = 1, [FromQuery] int pageSize = 1000)
        {
            var regions = await walksRepository.GetAllAsync(filterOn, filterValue, sortBy, ascending ?? true, page, pageSize);

            throw new Exception("Algo salio mal che");

            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.GetByIdAsync(id);

            if (walkDomainModel != null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<RegionDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalkDTO createWalkDTO)
        {
            var newWalk = mapper.Map<Walk>(createWalkDTO);
            var walkDomainModel = await walksRepository.CreateAsync(newWalk);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update(UpdateWalkDTO updateWalkDTO)
        {
            var updatedWalk = mapper.Map<Walk>(updateWalkDTO);
            var walkDomainModel = await walksRepository.UpdateAsync(updateWalkDTO.Id, updatedWalk);

            if (walkDomainModel != null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }
    }
}
