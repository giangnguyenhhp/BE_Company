using BE_Company.Models;
using BE_Company.Models.Request.Employee;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Services.Impl;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MasterDbContext _context;

    public EmployeeRepository(MasterDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAllEmployees()
    {
        return _context.Employee.Include(x => x.Company)
            .ThenInclude(x => x!.Departments)
            .ToList();
    }

    public Employee GetEmployeeById(long id)
    {
        var employee = _context.Employee.FirstOrDefault(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        return employee;
    }

    public Employee UpdateEmployee(long id, UpdateEmployee request)
    {
        var employee = _context.Employee.FirstOrDefault(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        var company = _context.Company.FirstOrDefault(x => x.CompanyId == request.CompanyId);
        if (company == null)
        {
            throw new Exception("Company is not existed");
        }

        var department = _context.Department.FirstOrDefault(x => x.DepartmentId == request.DepartmentId);
        if (department == null)
        {
            throw new Exception("Department is not existed");
        }

        employee.Name = request.Name;
        employee.Address = request.Address;
        employee.Birth = request.Birth;
        employee.Department = department;
        employee.Company = company;

        if (_context.Employee.Any(x => x.Company != null
                                       && x.Department != null
                                       && x.Name == request.Name
                                       && x.Address == request.Address
                                       && x.Birth == request.Birth
                                       && x.Department.DepartmentId == request.DepartmentId
                                       && x.Company.CompanyId == request.CompanyId
            ))
        {
            throw new Exception("Employee is existed");
        }

        _context.SaveChanges();
        return employee;
    }

    public Employee CreateEmployee(CreateEmployee request)
    {
        var employee = new Employee
        {
            Name = request.Name,
            Address = request.Address,
            Birth = request.Birth,
        };
        if (_context.Employee.Any(x => x.Name == request.Name
                                       && x.Address == request.Address
                                       && x.Birth == request.Birth))
        {
            throw new Exception("Employee already existed");
        }

        _context.Employee.Add(employee);
        _context.SaveChanges();
        return employee;
    }

    public Employee DeleteEmployee(long id)
    {
        var employee = _context.Employee.FirstOrDefault(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        _context.Employee.Remove(employee);
        _context.SaveChanges();
        return employee;
    }
}