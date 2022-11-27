using BE_Company.Models;
using BE_Company.Models.Request.Department;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Services.Impl;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly MasterDbContext _context;

    public DepartmentRepository(MasterDbContext context)
    {
        _context = context;
    }

    public List<Department> GetAllDepartment()
    {
        return _context.Department.Include(x=>x.Companies)!
            .ThenInclude(x=>x.Employees)
            .ToList();
    }

    public Department GetDepartment(long id)
    {
        var department = _context.Department.FirstOrDefault(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        return department;
    }

    public Department UpdateDepartment(long id, UpdateDepartment request)
    {
        var department = _context.Department
            .Include(x=>x.Companies)!
            .ThenInclude(x=>x.Employees)
            .FirstOrDefault(x => x.DepartmentId == id);

        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var companies = _context.Company
            .Where(x => request.CompanyName.Contains(x.NameCompany)).ToList();
        var employees = _context.Employee
            .Where(x => request.EmployeeName.Contains(x.Name)).ToList();

        department.Name = request.Name;
        department.NumberOf = request.NumberOf;
        department.Companies = companies;
        department.Employees = employees;

        if (_context.Department.Any(x => x.Name == request.Name
                                                && x.NumberOf == request.NumberOf))
        {
            throw new Exception("Department already exists");
        }

        _context.SaveChanges();
        return department;
    }

    public Department CreateDepartment(CreateDepartment request)
    {
        var department = new Department
        {
            Name = request.Name,
            NumberOf = request.NumberOf
        };
        if (_context.Department.Any(x => x.Name == request.Name))
        {
            throw new Exception("Department already existed");
        }
        _context.Department.Add(department);
        _context.SaveChanges();
        return department;
    }

    public Department DeleteCompany(long id)
    {
        var department = _context.Department.FirstOrDefault(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }
        
        _context.Department.Remove(department);
        _context.SaveChanges();
        return department;
    }

    public List<string> GetCompanyByDepartment(long id)
    {
        var department = _context.Department.FirstOrDefault(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var listCompany = _context.Company.Where(x => x.Departments.Contains(department))
            .Select(x => x.NameCompany)
            .ToList();
        return listCompany;
    }

    public List<string> GetEmployeeByDepartment(long id)
    {
        var department = _context.Department.FirstOrDefault(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var listEmployee = _context.Employee.Where(x => x.Department.Name == department.Name)
            .Select(x => x.Name)
            .ToList();
        return listEmployee;
    }
}