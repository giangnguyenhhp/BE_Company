using BE_Company.Models.Request.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Services.Authenticate;

public interface IUserRepository
{
    public Task<IActionResult> Register(RegisterRequest request);
}