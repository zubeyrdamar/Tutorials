using System.ComponentModel.DataAnnotations;

namespace ShopApp.API.Models.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
