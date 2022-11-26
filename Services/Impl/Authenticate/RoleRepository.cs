using System.Security.Claims;
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
        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var role in roles)
        {
            var claims = await _roleManager.GetClaimsAsync(role);
            role.RoleClaims = claims;
        }

        return roles;
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

    public async Task<Role> MapPermissions(MapPermissionsRequest request)
    {
        var checkRole = _roleManager.Roles.FirstOrDefault(x => x.Id == request.RoleId);
        if (checkRole == null)
        {
            throw new Exception("Role is not exist");
        }

        if (request.Permissions != null && !request.Permissions.Any()) throw new Exception("Permissions is not valid!!!");

        //Lấy ra các claim cũ trong role và xóa đi
        var listClaim = await _roleManager.GetClaimsAsync(checkRole);
        foreach (var claim in listClaim)
        {
            await _roleManager.RemoveClaimAsync(checkRole, claim);
        }

        //Add các claim mới vào trong role
        foreach (var claim in request.Permissions)
        {
            await _roleManager.AddClaimAsync(checkRole, new Claim(ClaimTypes.Role, claim));
        }
        await _dbContext.SaveChangesAsync();
        return checkRole;
    }
}