using BE_Company.Models.Auth;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Services.Impl.Authenticate;

public class AuthenticateRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AuthenticateRepository(UserManager<IdentityUser> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


}