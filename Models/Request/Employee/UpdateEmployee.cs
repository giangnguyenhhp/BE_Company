namespace BE_Company.Models.Request.Employee;

public class UpdateEmployee
{
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Birth { get; set; }
    public long DepartmentId { get; set; }
    public long CompanyId { get; set; }
}