using BE_Company.Models;
using BE_Company.Models.Request.Employee;
using BE_Company.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetAllEmployee()
        {
            return Ok(_employeeRepository.GetAllEmployees());
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            return Ok(_employeeRepository.GetEmployeeById(id));
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public IActionResult UpdateEmployee(long id,[FromBody] UpdateEmployee request)
        {
            return Ok(_employeeRepository.UpdateEmployee(id, request));
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreatEmployee([FromBody]CreateEmployee request)
        {
            return Ok(_employeeRepository.CreateEmployee(request));
        }

        // DELETE: api/Employee/5
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteEmployee(long id)
        {
            return Ok(_employeeRepository.DeleteEmployee(id));
        }
        

    }
}
