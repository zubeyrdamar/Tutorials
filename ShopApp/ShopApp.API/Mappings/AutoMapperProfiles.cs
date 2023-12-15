using AutoMapper;
using ShopApp.API.Models.DTO;
using ShopApp.Entities;

namespace ShopApp.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductDetailsDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
