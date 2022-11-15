using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Company.Models;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CompanyId { get; set; }
    public string NameCompany { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public List<Department>? Departments { get; set; }
    public List<Employee>? Employees { get; set; }
}