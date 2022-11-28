using BE_Company.Models;
using BE_Company.Models.Request.Company;

namespace BE_Company.Services;

public interface ICompanyRepository
{
    public Task<List<Company>> GetAllCompany();
    public Task<Company> GetCompanyById(long id);
    public Task<Company>  UpdateCompany(long id, UpdateCompany request);
    public Task<Company>  CreateCompany(CreateCompany request);
    public Task<Company>  DeleteCompany(long id);
    public Task<List<string>> GetDepartmentByCompany(long id);
    public Task<List<string>> GetEmployeeByCompany(long id);
}