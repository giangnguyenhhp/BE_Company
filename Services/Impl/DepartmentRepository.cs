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

    public async Task<List<Department>> GetAllDepartment()
    {
        return await _context.Department.Include(x=>x.Companies)!
            .ThenInclude(x=>x.Employees)
            .ToListAsync();
    }

    public async Task<Department> GetDepartment(long id)
    {
        var department =await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        return department;
    }

    public async Task<Department> UpdateDepartment(long id, UpdateDepartment request)
    {
        var department =await _context.Department
            .Include(x=>x.Companies)!
            .ThenInclude(x=>x.Employees)
            .FirstOrDefaultAsync(x => x.DepartmentId == id);

        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var companies =await _context.Company
            .Where(x => request.CompanyName.Contains(x.NameCompany)).ToListAsync();
        var employees =await _context.Employee
            .Where(x => request.EmployeeName.Contains(x.Name)).ToListAsync();

        department.Name = request.Name;
        department.NumberOf = request.NumberOf;
        department.Companies = companies;
        department.Employees = employees;

        if (_context.Department.Any(x => x.Name == request.Name
                                                && x.NumberOf == request.NumberOf))
        {
            throw new Exception("Department already exists");
        }

        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<Department> CreateDepartment(CreateDepartment request)
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
        await _context.Department.AddAsync(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<Department> DeleteCompany(long id)
    {
        var department =await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }
        
        _context.Department.Remove(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<List<string>> GetCompanyByDepartment(long id)
    {
        var department =await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var listCompany =await _context.Company.Where(x => x.Departments.Contains(department))
            .Select(x => x.NameCompany)
            .ToListAsync();
        return listCompany;
    }

    public async Task<List<string>> GetEmployeeByDepartment(long id)
    {
        var department =await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
        if (department == null)
        {
            throw new Exception("Department not existed!");
        }

        var listEmployee =await _context.Employee.Where(x => x.Department.Name == department.Name)
            .Select(x => x.Name)
            .ToListAsync();
        return listEmployee;
    }
}