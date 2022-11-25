using BE_Company.Models.Auth;
using BE_Company.Models.Request.Auth;
using BE_Company.Models.Request.User;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Controllers.Authenticate;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles =SystemPermissions.AdminAccess)]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userRepository.GetAllUsers());
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
    {
        return Ok(await _userRepository.Register(request));
    }

    [HttpPost("register-admin")]
    public async Task<ActionResult> RegisterAdmin([FromBody] RegisterUserRequest request)
    {
        return Ok(await _userRepository.RegisterAdmin(request));
    }

    [HttpPost("register-for-admin")]
    public async Task<ActionResult> RegisterForAdmin([FromBody] RegisterUserRequest request)
    {
        return Ok(await _userRepository.RegisterForAdmin(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        return Ok(await _userRepository.DeleteUser(id));
    }
}