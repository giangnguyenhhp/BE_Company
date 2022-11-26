using System.Security.Claims;

namespace BE_Company.Models.Request.Role;

public class MapPermissionsRequest
{
    public List<string> Permissions { get; set; }
    public string RoleId { get; set; }
    
}
