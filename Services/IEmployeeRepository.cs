using BE_Company.Models;
using BE_Company.Models.Request.Employee;

namespace BE_Company.Services;

public interface IEmployeeRepository
{
    public Task<List<Employee>> GetAllEmployees();
    public Task<Employee> GetEmployeeById(long id);
    public Task<Employee> UpdateEmployee(long id, UpdateEmployee request);
    public Task<Employee> CreateEmployee(CreateEmployee request);
    public Task<Employee> DeleteEmployee(long id);
}