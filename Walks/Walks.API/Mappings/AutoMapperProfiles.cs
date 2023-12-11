using AutoMapper;
using Walks.API.Models;
using Walks.API.Models.DTO;

namespace Walks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateRegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
        }
    }
}
