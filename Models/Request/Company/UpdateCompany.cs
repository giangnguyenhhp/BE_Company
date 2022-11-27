namespace BE_Company.Models.Request.Company;

public class UpdateCompany
{
    public string NameCompany { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public List<string>? EmployeeName { get; set; }
    public List<string>? DepartmentName { get; set; }
}