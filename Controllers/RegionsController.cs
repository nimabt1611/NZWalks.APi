using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;


namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class RegionsController : ControllerBase
	{
		
		private readonly NZWalksDbContext dbcontext;
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
		private readonly ILogger<RegionsController> logger;

		public RegionsController(NZWalksDbContext dbcontext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
	
			this.dbcontext = dbcontext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
			this.logger = logger;
		}
        [HttpGet]
	
		public async Task<IActionResult> GetAll()
		{

			logger.LogInformation("GetAll Regions Action Method was invoke");
			
			//Get data from data base - region models

			var regionsDomain = await regionRepository.GetAllAsync();

			//returns Dto
			logger.LogInformation($"Finised GetAll Regions Request With data : {JsonSerializer.Serialize(regionsDomain)}");

			return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

		}
		[HttpGet]
		[Route("{id:Guid}")]
		[Authorize(Roles ="Reader")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{

			var regionDomain = await regionRepository.GetByIdAsync(id);

			if(regionDomain == null)
			{
				return NotFound();
			}


				return Ok(mapper.Map<RegionDto>(regionDomain));
		
		}
		[HttpPost]
		[ValidateModel]
		[Authorize(Roles ="Writer")]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			
				var regionDomain = mapper.Map<Region>(addRegionRequestDto);
				regionDomain = await regionRepository.CreateAsync(regionDomain);

				var regionDto = mapper.Map<RegionDto>(regionDomain);

				return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, regionDto);
		
		}
		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]
		[Authorize(Roles ="Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionDto updateRegionDto)
		{
				var regionDomain = mapper.Map<Region>(updateRegionDto);
				regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

				if (regionDomain == null)
				{
					return NotFound();
				}

				return Ok(mapper.Map<RegionDto>(regionDomain));
	
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		[Authorize(Roles ="Writer")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var regionDomain = await regionRepository.DeleteAsync(id); 


			if(regionDomain==null)
			{
				return NotFound();
			}
				

			return Ok(mapper.Map<RegionDto>(regionDomain));
		}

			
	}
}
