namespace ShopApp.API.Models.DTO.Auth
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
