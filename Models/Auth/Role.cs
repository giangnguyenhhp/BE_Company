using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BE_Company.Models.Auth;

public class Role : IdentityRole
{
    public Role() {}
    public Role(string Name) : base(Name) {}
    [NotMapped]
    public virtual ICollection<Claim> RoleClaims { get; set; }

}