using BE_Company.Models.Auth;
using BE_Company.Models.Request.Role;

namespace BE_Company.Services.Authenticate;

public interface IRoleRepository
{
    public Task<List<Role>> GetAllRoles();
    public Task<Role> CreateRole(CreateRoleRequest request);
    public Task<Role> UpdateRole(string id, UpdateRoleRequest request);
    public Task<Role> DeleteRole(string id);
}