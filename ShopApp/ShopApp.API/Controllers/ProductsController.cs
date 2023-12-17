using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.API.Models.DTO;
using ShopApp.Business.Abstract;
using ShopApp.Entities;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public ProductsController(IMapper mapper, IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var products = productService.List();
            var productsDTO = mapper.Map<List<ProductDTO>>(products);
            return Ok(productsDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Read(int id)
        {
            var product = productService.Read(id);
            if (product == null) { return NotFound(); }
            var productDTO = mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details([FromRoute] int id)
        {
            var product = productService.Details(id);
            if (product == null) { return NotFound(); }
            var productDTO = new ProductDetailsDTO()
            {
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Categories = mapper.Map<List<CategoryDTO>>(product.ProductCategories.Select(c => c.Category).ToList()),
            };

            return Ok(productDTO);
        }

        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            if (productDTO == null) { return BadRequest(); }
            var product = mapper.Map<Product>(productDTO);
            productService.Create(product);
            return Ok(productDTO);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            var tempProduct = productService.Read(product.Id);
            if (tempProduct == null) { return BadRequest(); }

            tempProduct.Name = product.Name;
            tempProduct.ImageUrl = product.ImageUrl;
            tempProduct.Price = product.Price;
            productService.Update(tempProduct);

            var productDTO = mapper.Map<ProductDTO>(tempProduct);
            return Ok(productDTO);
        }

        [HttpDelete]
        [Route("details/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = productService.Read(id);
            if(product == null) { return BadRequest(); }

            productService.Delete(product);
            var productDTO = mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }
    }
}
