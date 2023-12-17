﻿namespace ShopApp.API.Models.DTO
{
    public class ProductDetailsDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}
