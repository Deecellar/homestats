using Microsoft.AspNetCore.Identity;

namespace backend.Models.Identity
{
    public class Role : IdentityRole
{
}

public enum Roles
{
    Admin,
    Basic,
}
}