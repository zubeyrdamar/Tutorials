using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ApiDbContext _context = new();

        [HttpGet]
        [Authorize]
        public IActionResult List()
        {
            return StatusCode(StatusCodes.Status200OK, _context.Categories.ToList());
        }
    }
}
