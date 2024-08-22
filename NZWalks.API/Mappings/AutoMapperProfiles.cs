using AutoMapper;
using Microsoft.Identity.Client;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateRegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, CreateWalkDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();

        }
    }
}
