using BE_Company.Models;
using BE_Company.Models.Request.Department;


namespace BE_Company.Services;

public interface IDepartmentRepository
{
    public Task<List<Department>> GetAllDepartment();
    public Task<Department> GetDepartment(long id);
    public Task<Department> UpdateDepartment(long id, UpdateDepartment request);
    public Task<Department> CreateDepartment(CreateDepartment request);
    public Task<Department> DeleteCompany(long id);
    public Task<List<string>> GetCompanyByDepartment(long id);
    public Task<List<string>> GetEmployeeByDepartment(long id);
}