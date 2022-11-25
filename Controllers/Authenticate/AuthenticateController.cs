using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BE_Company.Models;
using BE_Company.Models.Auth;
using BE_Company.Models.Request.Auth;
using Microsoft.AspNetCore.Identity;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BE_Company.Controllers.Authenticate;

[ApiController]
[Route("api/[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly MasterDbContext _dbContext;

    public AuthenticateController( IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager, MasterDbContext dbContext)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }
    //Method GetToken when login
    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));
        var token = new JwtSecurityToken
        (
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: authClaims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
    
    // TODO : Get token when login
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password)) return Unauthorized();
        
        //Lấy tên tất cả role trong user đăng nhâp : GetRoleAsync,sau đó lấy ra list các role để check claim
        var roles = await _userManager.GetRolesAsync(user);
        var roleList = await _dbContext.Roles.Where(x=>x.Name != null && roles.Contains(x.Name)).ToListAsync();

        if (user.UserName == null) return Unauthorized();
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
                
        //Lấy role của user để add vào authClaims trong token
        authClaims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role,userRole)));
        
        //lấy các claim trong role để add vào authClaims trong token
        var claims = new List<Claim>();
        foreach (var role in roleList)
        {
            var claimInRole = await _roleManager.GetClaimsAsync(role);
            if (claimInRole.Any())
            {
               claims.AddRange(claimInRole);
            }
        }
        
        
        //Nếu claims khác null thì add vào authClaims
        if (claims.Any())
        {
            authClaims.AddRange(claims);
        }

        var token = GetToken(authClaims);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            claims = claims.Select(x=>x.Value)
        });

    }
    
}