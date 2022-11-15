using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Company.Models;

public class Department
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DepartmentId { get; set; }
    public string Name { get; set; }
    public int NumberOf { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<Company>? Companies { get; set; }
}