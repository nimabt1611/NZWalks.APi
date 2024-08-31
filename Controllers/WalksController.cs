using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalksController(IMapper mapper ,IWalkRepository walkRepository)
		{
			this.mapper = mapper;
			this.walkRepository = walkRepository;
		}

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			
				var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

				await walkRepository.CreateAsync(walkDomainModel);

				return Ok(mapper.Map<WalkDto>(walkDomainModel));
		
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? Filteron, [FromQuery] string? FilterQuery , [FromQuery] string? SortedBy , [FromQuery] bool? IsAscending)
		
		{
			var walkDomainModel = await walkRepository.GetAllAsync(Filteron , FilterQuery , SortedBy , IsAscending ?? true);




			return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
		}
		[HttpGet("{id:Guid}")]
		
		public async Task<IActionResult> GetById(Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIdASync(id);

			if(walkDomainModel ==null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}
		[HttpPut("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update(Guid id , UpdateWalkDto updateWalkDto)
		{
		
				var walksDomainModel = mapper.Map<Walk>(updateWalkDto);
				walksDomainModel = await walkRepository.UpdateAsync(id, walksDomainModel);

				if (walksDomainModel == null)
				{
					return NotFound();
				}

				return Ok(mapper.Map<WalkDto>(walksDomainModel));
			
		}
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var walkDomainModel = await walkRepository.DeleteAsync(id);
			if(walkDomainModel==null)
			{
				return NotFound();
			}
			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

	}
}
