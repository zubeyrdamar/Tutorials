using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ApiDbContext _context = new();

        [HttpGet]
        public IActionResult List()
        {
            return StatusCode(StatusCodes.Status200OK, _context.Categories);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, "Category has been created");
        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            var tempCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(tempCategory != null)
            {
                return StatusCode(StatusCodes.Status200OK, tempCategory);
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            var tempCategory = _context.Categories.Find(id);
            if(tempCategory != null)
            {
                tempCategory.Name = category.Name;
                tempCategory.Description = category.Description;
                tempCategory.ImageUrl = category.ImageUrl;
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Category has been updated");
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tempCategory = _context.Categories.Find(id);
            if (tempCategory != null)
            {
                _context.Categories.Remove(tempCategory);
                _context.SaveChanges(); 
                return StatusCode(StatusCodes.Status200OK, "Category has been deleted");
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }
    }
}
