using BE_Company.Models;
using BE_Company.Models.Request.Company;

namespace BE_Company.Services;

public interface ICompanyRepository
{
    public List<Company> GetAllCompany();
    public Company GetCompanyById(long id);
    public Company UpdateCompany(long id, UpdateCompany request);
    public Company CreateCompany(CreateCompany request);
    public Company DeleteCompany(long id);
    public List<string> GetDepartmentByCompany(long id);
    public List<string> GetEmployeeByCompany(long id);
}