namespace BE_Company.Models.Request.Department;

public class UpdateDepartment
{
    public string Name { get; set; }
    public int NumberOf { get; set; }
    public List<string>? EmployeeName { get; set; }
    public List<string>? CompanyName { get; set; }
}