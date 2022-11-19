using BE_Company.Models.Request.Role;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Controllers.Authenticate;
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        return Ok(await _roleRepository.GetAllRoles());
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        return Ok(await _roleRepository.CreateRole(request));
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleRequest request)
    {
        return Ok(await _roleRepository.UpdateRole(id, request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        return Ok(await _roleRepository.DeleteRole(id));
    }
}