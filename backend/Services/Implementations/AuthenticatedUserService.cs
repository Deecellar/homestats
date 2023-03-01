using System.Security.Claims;

namespace backend.Services.Implementations
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public Claim? UserId {get; }

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) => UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid") ?? new Claim("uid", "OwO");
    }
}
