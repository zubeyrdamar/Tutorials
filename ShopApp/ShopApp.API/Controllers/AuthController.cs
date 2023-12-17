using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.API.Identity;
using ShopApp.API.Models.DTO.Auth;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Fullname = registerDTO.Fullname,
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if(result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(loginDTO.Username);
            if(user == null)
            {
                return BadRequest("User does not exist");
            }

            var result = await signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest("Invalid Credentials");
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

    }
}
