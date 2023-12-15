using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult List()
        {
            return Ok(productService.List());
        }
    }
}
