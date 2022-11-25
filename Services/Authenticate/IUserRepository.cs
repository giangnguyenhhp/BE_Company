using BE_Company.Models.Auth;
using BE_Company.Models.Request.Auth;
using BE_Company.Models.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Services.Authenticate;

public interface IUserRepository
{
    public Task<IActionResult> Register(RegisterUserRequest request);
    public Task<IActionResult> RegisterAdmin(RegisterUserRequest request);
    public Task<List<User>> GetAllUsers();
    public Task<IActionResult> DeleteUser(string id);
    public Task<IActionResult> RegisterForAdmin(RegisterUserRequest request);
}