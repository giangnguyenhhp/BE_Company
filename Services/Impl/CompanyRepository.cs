using BE_Company.Models;
using BE_Company.Models.Request.Company;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Services.Impl;

public class CompanyRepository : ICompanyRepository
{
    private readonly MasterDbContext _context;

    public CompanyRepository(MasterDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllCompany()
    {
        return await _context.Company.Include(x => x.Departments)!
            .ThenInclude(x => x.Employees)
            .ToListAsync();
    }

    public async Task<Company> GetCompanyById(long id)
    {
        var company =await _context.Company.FindAsync(id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        return company;
    }

    public async Task<Company> UpdateCompany(long id, UpdateCompany request)
    {
        var company =await _context.Company
            .Include(x => x.Departments)!
            .ThenInclude(x => x.Employees)
            .FirstOrDefaultAsync(x => x.CompanyId == id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        var employees =await _context.Employee
            .Where(x => request.EmployeeName.Contains(x.Name)).ToListAsync();
        var departments =await _context.Department
            .Where(x => request.DepartmentName.Contains(x.Name)).ToListAsync();

        company.NameCompany = request.NameCompany;
        company.Address = request.Address;
        company.Description = request.Description;
        company.Departments = departments;
        company.Employees = employees;

        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> CreateCompany(CreateCompany request)
    {
        var company = new Company
        {
            Address = request.Address,
            NameCompany = request.NameCompany,
            Description = request.Description,
        };

        if (_context.Company.Any(x => x.Address == request.Address
                                      && x.NameCompany == request.NameCompany
                                      && x.Description == request.Description))
        {
            throw new Exception("Company already existed.");
        }

        await _context.Company.AddAsync(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> DeleteCompany(long id)
    {
        var company =await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        _context.Company.Remove(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<List<string>> GetDepartmentByCompany(long id)
    {
        var company =await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        var departments =await _context.Department
            .Where(x => x.Companies.Contains(company))
            .Select(x => x.Name).ToListAsync();
        return departments;
    }

    public async Task<List<string>> GetEmployeeByCompany(long id)
    {
        var company =await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        var employees =await _context.Employee
            .Where(x => x.Company.CompanyId == id).Select(x => x.Name).ToListAsync();
        return employees;
    }
}