using System;
using System.Security.Claims;

namespace backend.Services
{
    public interface IAuthenticatedUserService
    {
        Claim? UserId { get; }
    }
}
