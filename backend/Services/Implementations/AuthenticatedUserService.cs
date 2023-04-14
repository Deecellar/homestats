using System.Security.Claims;

namespace backend.Services.Implementations;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirst("uid") ?? new Claim("uid", "OwO");
    }

    public Claim? UserId { get; }
}