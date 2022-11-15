namespace BE_Company.Models.Request.Employee;

public class CreateEmployee
{
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Birth { get; set; }
}