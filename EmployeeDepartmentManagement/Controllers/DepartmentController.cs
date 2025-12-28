using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDepartmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository repo;

        public DepartmentController(DepartmentRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartment(string dName)
        {
            Department department = new Department()
            {
                DName = dName
            };
            var res = await repo.AddDepartment(department);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Department already exists");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var res = await repo.GetAllDepartments();
            return Ok(res);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var res = await repo.GetDepartmentById(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Department not found");
        }
    }
}
