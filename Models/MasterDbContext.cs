using Microsoft.EntityFrameworkCore;

namespace BE_Company.Models;

public class MasterDbContext : DbContext
{
    public DbSet<Company> Company { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Department> Department { get; set; }
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
        
    }
}