using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UsersController : Controller
    {
        readonly ApiDbContext _context = new ApiDbContext();
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            var userExist = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if(userExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User already exists");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, "User has been registered successfully");
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var tempUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if(tempUser == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User not found");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{ new Claim(ClaimTypes.Email, user.Email) };
            var jwt = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                                                    audience: _configuration["JWT:Audience"],
                                                                                    claims: claims,
                                                                                    expires: DateTime.Now.AddMinutes(60),
                                                                                    signingCredentials: credentials));

            return StatusCode(StatusCodes.Status200OK, jwt);
        }
    }
}
