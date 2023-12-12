using Microsoft.AspNetCore.Identity;

namespace Walks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWT(IdentityUser user, List<string> roles);
    }
}
