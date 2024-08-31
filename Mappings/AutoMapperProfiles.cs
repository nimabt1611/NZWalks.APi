﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
			CreateMap<UpdateRegionDto, Region>().ReverseMap();
			CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();
			CreateMap<UpdateWalkDto , Walk>().ReverseMap();

		}
	}
}
