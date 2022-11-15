using BE_Company.Models;
using BE_Company.Models.Request.Department;


namespace BE_Company.Services;

public interface IDepartmentRepository
{
    public List<Department> GetAllDepartment();
    public Department GetDepartment(long id);
    public Department UpdateDepartment(long id, UpdateDepartment request);
    public Department CreateDepartment(CreateDepartment request);
    public Department DeleteCompany(long id);
}