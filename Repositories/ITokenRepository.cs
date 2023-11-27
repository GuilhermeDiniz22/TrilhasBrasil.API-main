using Microsoft.AspNetCore.Identity;

namespace TrilhasBrasil.API.Repositories
{
    public interface ITokenRepository
    {
        string CriarJwtToken(IdentityUser user, List<string> roles);
    }
}
