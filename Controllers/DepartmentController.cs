using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_Company.Models;
using BE_Company.Models.Auth;
using BE_Company.Models.Request.Department;
using BE_Company.Services;
using Microsoft.AspNetCore.Authorization;


namespace BE_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = SystemPermissions.ReadDepartment)]
        public async Task<ActionResult> GetAllDepartment()
        {
            return Ok(await _departmentRepository.GetAllDepartment());
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        [Authorize(Roles = SystemPermissions.ReadDepartment)]
        public async Task<ActionResult> GetDepartment(long id)
        {
            return Ok(await _departmentRepository.GetDepartment(id));
        }

        [HttpGet("get-company-by-department/{id}")]
        [Authorize(Roles = SystemPermissions.ReadCompany)]
        public async Task<ActionResult> GetCompanyByDepartment(long id)
        {
            return Ok(await _departmentRepository.GetCompanyByDepartment(id));
        }

        [HttpGet("get-employee-by-department/{id}")]
        [Authorize(Roles = SystemPermissions.ReadEmployee)]
        public async Task<ActionResult> GetEmployeeByDepartment(long id)
        {
            return Ok(await _departmentRepository.GetEmployeeByDepartment(id));
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize(Roles = SystemPermissions.UpdateDepartment)]
        public async Task<ActionResult> UpdateDepartment(long id, [FromBody] UpdateDepartment request)
        {
            return Ok(await _departmentRepository.UpdateDepartment(id, request));
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = SystemPermissions.CreateDepartment)]
        public async Task<ActionResult> CreateDepartment([FromBody] CreateDepartment request)
        {
            return Ok(await _departmentRepository.CreateDepartment(request));
        }

        // DELETE: api/Department/5
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = SystemPermissions.DeleteDepartment)]
        public async Task<ActionResult> DeleteCompany(long id)
        {
            return Ok(await _departmentRepository.DeleteCompany(id));
        }
    }
}