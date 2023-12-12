using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.DTO.Auth;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> manager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> manager, ITokenRepository tokenRepository)
        {
            this.manager = manager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var user = new IdentityUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Username
            };

            var result = await manager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                if(registerDTO.Roles != null && registerDTO.Roles.Any())
                {
                    result = await manager.AddToRolesAsync(user, registerDTO.Roles);
                    if (result.Succeeded) { return Ok("Success"); }
                }
            }

            return BadRequest("Fail");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await manager.FindByEmailAsync(loginDTO.Username);
            if (user != null && await manager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var roles = await manager.GetRolesAsync(user);
                if(roles != null && roles.Any())
                {
                    var jwt = tokenRepository.CreateJWT(user, roles.ToList());
                    return Ok(jwt);
                }
            }

            return BadRequest("Invalid Credentials");
        }
    }
}
