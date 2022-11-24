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
    public IActionResult GetAllCompany()
    {
        return Ok(_companyRepository.GetAllCompany());
    }

    // GET: api/Company/5
    [HttpGet("{id}")]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public IActionResult GetCompany(long id)
    {
        return Ok(_companyRepository.GetCompanyById(id));
    }

    [HttpGet("get-department-by-company")]
    [Authorize(Roles = SystemPermissions.ReadCompany)]
    public IActionResult GetDepartmentByCompany()
    {
        return Ok(_companyRepository.GetDepartmentByCompany());
    }

    // PUT: api/Company/update/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("update/{id}")]
    [Authorize(Roles = SystemPermissions.UpdateCompany)]
    public IActionResult PutCompany(long id, [FromBody] UpdateCompany request)
    {
        return Ok(_companyRepository.UpdateCompany(id, request));
    }

    // POST: api/Company
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = SystemPermissions.CreateCompany)]
    public IActionResult PostCompany([FromBody] CreateCompany request)
    {
        return Ok(_companyRepository.CreateCompany(request));
    }

    // DELETE: api/Company/delete/5
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = SystemPermissions.DeleteCompany)]
    public IActionResult DeleteCompany(long id)
    {
        return Ok(_companyRepository.DeleteCompany(id));
    }
}