using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
