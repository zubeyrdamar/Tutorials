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
        public IEnumerable<Category> List()
        {
            return _context.Categories;
        }

        [HttpPost]
        public void Create([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        [HttpGet("{id}")]
        public Category Read(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Category category)
        {
            var tempCategory = _context.Categories.Find(id);
            if(tempCategory != null)
            {
                tempCategory.Name = category.Name;
                tempCategory.Description = category.Description;
                tempCategory.ImageUrl = category.ImageUrl;
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tempCategory = _context.Categories.Find(id);
            if (tempCategory != null)
            {
                _context.Categories.Remove(tempCategory);
                _context.SaveChanges();
            }
        }
    }
}
