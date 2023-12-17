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
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new
                {
                    UserId = user.Id,
                    Token = token
                });

                return Ok(callbackUrl);
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
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Email is not confirmed");
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

        [HttpPost]
        [Route("get/confirmation/url")]
        public async Task<IActionResult> CorfirmationUrl(EmailConfirmationDTO emailConfirmationDTO)
        {
            var user = await userManager.FindByNameAsync(emailConfirmationDTO.Username);
            if (user == null) { return NotFound(); }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new
            {
                UserId = user.Id,
                Token = token
            });

            return Ok(callbackUrl);
        }

        [HttpPost]
        [Route("confirm/email")]
        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return NotFound("Invalid Url");
            }

            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound("Invalid User");
            }

            var result = await userManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                return Ok("Account is confirmed");
            }

            return BadRequest("Something went wrong");
        }


        [HttpPost]
        [Route("forgot/password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            if (string.IsNullOrEmpty(forgotPasswordDTO.Email))
            {
                return BadRequest("Enter a valid email");
            }

            var user = await userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null) { return NotFound("Email is not registered"); }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Auth", new
            {
                UserId = user.Id,
                Token = token
            });

            return Ok(callbackUrl);
        }

        [HttpPost]
        [Route("reset/password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null) { return NotFound("Email is not registered"); }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);
            if (result.Succeeded) 
            {
                return Ok("Password has been renewed");
            }

            return BadRequest("Something went wrong");
        }
    }
}
