using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.Security.Claims;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApiDbContext _context = new();

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Property property)
        {
            if(property == null) { return StatusCode(StatusCodes.Status204NoContent, "No Content Found"); }

            var userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == userMail);
            if(user == null) { return StatusCode(StatusCodes.Status404NotFound, "User not found"); }

            property.IsTrending = false;
            property.UserId = user.Id;
            _context.Properties.Add(property);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, "Created");
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Property property)
        {
            var tempProperty = _context.Properties.FirstOrDefault(p => p.Id == id);
            if (tempProperty == null) { return StatusCode(StatusCodes.Status404NotFound, "No Content Found"); }

            var userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == userMail);
            if (user == null) { return StatusCode(StatusCodes.Status404NotFound, "User not found"); }
            if(tempProperty.UserId != user.Id) { return StatusCode(StatusCodes.Status405MethodNotAllowed, "User has no access"); }

            tempProperty.Name = property.Name;
            tempProperty.Detail = property.Detail;
            tempProperty.Price = property.Price;
            tempProperty.Address = property.Address;
            tempProperty.ImageUrl = property.ImageUrl;
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, "Updated");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var tempProperty = _context.Properties.FirstOrDefault(p => p.Id == id);
            if (tempProperty == null) { return StatusCode(StatusCodes.Status404NotFound, "No Content Found"); }

            var userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == userMail);
            if (user == null) { return StatusCode(StatusCodes.Status404NotFound, "User not found"); }
            if (tempProperty.UserId != user.Id) { return StatusCode(StatusCodes.Status405MethodNotAllowed, "User has no access"); }

            _context.Properties.Remove(tempProperty);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, "Deleted");
        }

        [HttpGet("{categoryId}")]
        public IActionResult List(int categoryId)
        {
            var tempProperties = _context.Properties.Where(c => c.CategoryId == categoryId);
            if (!tempProperties.Any()) { return StatusCode(StatusCodes.Status404NotFound, "Nothing found to list"); }

            return StatusCode(StatusCodes.Status200OK, tempProperties);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var tempProperty = _context.Properties.FirstOrDefault(p => p.Id == id);
            if (tempProperty == null) { return StatusCode(StatusCodes.Status404NotFound, "Nothing found to show"); }

            return StatusCode(StatusCodes.Status200OK, tempProperty);
        }
    }
}
