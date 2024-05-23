using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Identity;

public class AppUser: IdentityUser
{
    public string Role { get; set; }
}