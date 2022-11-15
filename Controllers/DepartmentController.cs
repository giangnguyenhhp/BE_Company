using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_Company.Models;
using BE_Company.Models.Request.Department;
using BE_Company.Services;


namespace BE_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly MasterDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(MasterDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        // GET: api/Department
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            return Ok(_departmentRepository.GetAllDepartment());
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public IActionResult GetDepartment(long id)
        {
            return Ok(_departmentRepository.GetDepartment(id));
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(long id,[FromBody] UpdateDepartment request)
        {
            return Ok(_departmentRepository.UpdateDepartment(id, request));
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] CreateDepartment request)
        {
            return Ok(_departmentRepository.CreateDepartment(request));
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(long id)
        {
            return Ok(_departmentRepository.DeleteCompany(id));
        }

        private bool CompanyExists(long id)
        {
            return (_context.Company?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}
