namespace BE_Company.Models.Request.Department;

public class UpdateDepartment
{
    public string Name { get; set; }
    public int NumberOf { get; set; }
    public List<long>? EmployeeId { get; set; }
    public List<long>? CompanyId { get; set; }
}