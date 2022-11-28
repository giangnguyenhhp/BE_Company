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

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _context.Employee.Include(x => x.Company)
            .ThenInclude(x => x!.Departments)
            .ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(long id)
    {
        var employee =await _context.Employee.FirstOrDefaultAsync(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        return employee;
    }

    public async Task<Employee> UpdateEmployee(long id, UpdateEmployee request)
    {
        var employee =await _context.Employee.FirstOrDefaultAsync(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        var company =await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == request.CompanyId);
        if (company == null)
        {
            throw new Exception("Company is not existed");
        }

        var department =await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == request.DepartmentId);
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

        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> CreateEmployee(CreateEmployee request)
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

        await _context.Employee.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> DeleteEmployee(long id)
    {
        var employee =await _context.Employee.FirstOrDefaultAsync(x => x.EmployeeId == id);
        if (employee == null)
        {
            throw new Exception("Employee is not existed");
        }

        _context.Employee.Remove(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
}