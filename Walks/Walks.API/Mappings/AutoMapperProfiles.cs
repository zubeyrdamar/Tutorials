using AutoMapper;
using Walks.API.Models;
using Walks.API.Models.DTO;

namespace Walks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, CreateWalkDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkDTO>().ReverseMap();

            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateRegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<Difficulty, CreateDifficultyDTO>().ReverseMap();
            CreateMap<Difficulty, UpdateDifficultyDTO>().ReverseMap();
        }
    }
}
