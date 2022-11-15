using BE_Company.Models;
using BE_Company.Models.Request.Employee;

namespace BE_Company.Services;

public interface IEmployeeRepository
{
    public List<Employee> GetAllEmployees();
    public Employee GetEmployeeById(long id);
    public Employee UpdateEmployee(long id, UpdateEmployee request);
    public Employee CreateEmployee(CreateEmployee request);
    public Employee DeleteEmployee(long id);
}