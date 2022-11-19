using BE_Company.Models;
using BE_Company.Models.Auth;
using BE_Company.Models.Request.Role;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Services.Impl.Authenticate;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<Role> _roleManager;
    private readonly MasterDbContext _dbContext;

    public RoleRepository(RoleManager<Role> roleManager, MasterDbContext dbContext)
    {
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<List<Role>> GetAllRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<Role> CreateRole(CreateRoleRequest request)
    {
        var roleList = _roleManager.Roles.ToList();
        
        if (roleList.Any(x=>x.Name==request.Name))
        {
            throw new Exception("Role is existed");
        }
        
        Role role = new()
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = request.Name.ToUpper(),
        };

        await _roleManager.CreateAsync(role);
        await _dbContext.SaveChangesAsync();
        return role;
    }

    public async Task<Role> UpdateRole(string id, UpdateRoleRequest request)
    {
        var checkRole = _roleManager.Roles.FirstOrDefault(x=>x.Id == id);
        var roleList = _roleManager.Roles.ToList();
        if (checkRole==null)
        {
            throw new Exception("Role is not exist");
        }
        
        if (roleList.Any(x=> x.Name == request.Name))
        {
            throw new Exception($"Role {request.Name} is already existed");
        }
        
        checkRole.Name = request.Name;
        checkRole.NormalizedName = request.Name.ToUpper();

        await _dbContext.SaveChangesAsync();
        return checkRole;
    }

    public async Task<Role> DeleteRole(string id)
    {
        var checkRole = _roleManager.Roles.FirstOrDefault(x=>x.Id == id);
        if (checkRole==null)
        {
            throw new Exception("Role is not exist");
        }
        
        await _roleManager.DeleteAsync(checkRole);
        await _dbContext.SaveChangesAsync();
        return checkRole;
    }
}