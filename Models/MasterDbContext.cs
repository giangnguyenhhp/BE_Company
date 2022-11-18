using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BE_Company.Models;

public class MasterDbContext : IdentityDbContext<IdentityUser<string>,IdentityRole<string>,string,IdentityUserClaim<string>,IdentityUserRole<string>,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
{
    public DbSet<Company> Company { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Department> Department { get; set; }
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        base.OnModelCreating(modelbuilder);
        foreach (var entityType in modelbuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}