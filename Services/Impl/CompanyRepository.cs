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

    public List<Company> GetAllCompany()
    {
        return  _context.Company.Include(x=>x.Departments)!
            .ThenInclude(x=>x.Employees)
            .ToList();
    }

    public Company GetCompanyById(long id)
    {
        var company =  _context.Company.Find(id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }
        return company;
    }

    public Company UpdateCompany(long id, UpdateCompany request)
    {
        var company = _context.Company
            .Include(x=>x.Departments)!
            .ThenInclude(x=>x.Employees)
            .FirstOrDefault(x => x.CompanyId == id);
        if (company == null)
        {
            throw new Exception("Company not existed.");
        }

        var employees = _context.Employee
            .Where(x => request.EmployeeId.Contains(x.EmployeeId)).ToList();
        var departments = _context.Department
            .Where(x => request.DepartmentId.Contains(x.DepartmentId)).ToList();

        company.NameCompany = request.NameCompany;
        company.Address = request.Address;
        company.Description = request.Description;
        company.Departments = departments;
        company.Employees = employees;
        
        if (_context.Company.Any(x => x.Address == request.Address
                                      && x.NameCompany == request.NameCompany
                                      && x.Description == request.Description))
        {
            throw new Exception("Company already existed.");
        }
        
        _context.SaveChanges();
        return company;
    }

    public Company CreateCompany(CreateCompany request)
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
        
        _context.Company.Add(company);
        _context.SaveChanges();
        return company;
    }

    public Company DeleteCompany(long id)
    {
        var company = _context.Company.FirstOrDefault(x => x.CompanyId == id);
        if (company==null)
        {
            throw new Exception("Company not existed.");
        }

        _context.Company.Remove(company);
         _context.SaveChanges();
        return company;
    }

    public List<Department> GetDepartmentByCompany()
    {
        throw new NotImplementedException();
    }
}