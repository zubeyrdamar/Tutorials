using Microsoft.AspNetCore.Identity;

namespace ShopApp.API.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
