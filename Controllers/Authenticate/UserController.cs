using BE_Company.Models.Request.Auth;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Controllers.Authenticate;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        return Ok(await _userRepository.Register(request));
    }
}