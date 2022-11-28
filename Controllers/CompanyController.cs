using BE_Company.Models.Auth;
using BE_Company.Models.Request.Company;
using BE_Company.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    // GET: api/Company
    [HttpGet]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public async Task<IActionResult> GetAllCompany()
    {
        return Ok(await _companyRepository.GetAllCompany());
    }

    // GET: api/Company/5
    [HttpGet("{id}")]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public async Task<IActionResult> GetCompany(long id)
    {
        return Ok(await _companyRepository.GetCompanyById(id));
    }

    [HttpGet("get-department-by-company/{id}")]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public async Task<IActionResult> GetDepartmentByCompany(long id)
    {
        return Ok(await _companyRepository.GetDepartmentByCompany(id));
    }

    [HttpGet("get-employee-by-company/{id}")]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public async Task<IActionResult> GetEmployeeByCompany(long id)
    {
        return Ok(await _companyRepository.GetEmployeeByCompany(id));
    }

    // PUT: api/Company/update/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("update/{id}")]
    [Authorize(Roles = SystemPermissions.UpdateCompany)]
    public async Task<IActionResult> PutCompany(long id, [FromBody] UpdateCompany request)
    {
        return Ok(await _companyRepository.UpdateCompany(id, request));
    }

    // POST: api/Company
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = SystemPermissions.CreateCompany)]
    public async Task<IActionResult> PostCompany([FromBody] CreateCompany request)
    {
        return Ok(await _companyRepository.CreateCompany(request));
    }

    // DELETE: api/Company/delete/5
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = SystemPermissions.DeleteCompany)]
    public async Task<IActionResult> DeleteCompany(long id)
    {
        return Ok(await _companyRepository.DeleteCompany(id));
    }
}