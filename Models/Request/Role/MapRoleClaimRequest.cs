using System.Security.Claims;

namespace BE_Company.Models.Request.Role;

public class MapRoleClaimRequest
{
    public List<string> RoleClaims { get; set; }
    
}
