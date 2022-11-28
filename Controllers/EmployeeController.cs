using BE_Company.Models;
using BE_Company.Models.Auth;
using BE_Company.Models.Request.Employee;
using BE_Company.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly MasterDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(MasterDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        // GET: api/Employee
        [HttpGet]
        [Authorize(Roles = SystemPermissions.ReadEmployee)]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeRepository.GetAllEmployees());
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [Authorize(Roles = SystemPermissions.ReadEmployee)]
        public async Task<IActionResult> GetEmployee(long id)
        {
            return Ok(await _employeeRepository.GetEmployeeById(id));
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize(Roles = SystemPermissions.UpdateEmployee)]
        public async Task<IActionResult> UpdateEmployee(long id,[FromBody] UpdateEmployee request)
        {
            return Ok(await _employeeRepository.UpdateEmployee(id, request));
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = SystemPermissions.CreateEmployee)]
        public async Task<IActionResult> CreatEmployee([FromBody]CreateEmployee request)
        {
            return Ok(await _employeeRepository.CreateEmployee(request));
        }

        // DELETE: api/Employee/5
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = SystemPermissions.DeleteEmployee)]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            return Ok(await _employeeRepository.DeleteEmployee(id));
        }
        

    }
}
