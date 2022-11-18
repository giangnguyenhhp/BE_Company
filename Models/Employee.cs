using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Company.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Birth { get; set; }
    public Department? Department { get; set; }
    public Company? Company { get; set; }
}