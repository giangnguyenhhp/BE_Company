using BE_Company.Models;
using BE_Company.Models.Request.Company;
using BE_Company.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly MasterDbContext _context;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(MasterDbContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }

        // GET: api/Company
        [HttpGet]
        public IActionResult GetAllCompany()
        {
            return Ok(_companyRepository.GetAllCompany());
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public IActionResult GetCompany(long id)
        {
            return Ok(_companyRepository.GetCompanyById(id));
        }

        // PUT: api/Company/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCompany(long id, [FromBody] UpdateCompany request)
        {
            return Ok(_companyRepository.UpdateCompany(id, request));
        }

        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCompany([FromBody] CreateCompany request)
        {
            return Ok(_companyRepository.CreateCompany(request));
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(long id)
        {
            return Ok(_companyRepository.DeleteCompany(id));
        }
        
    }
}