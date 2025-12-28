using EmployeeDepartmentManagement.Dto;
using EmployeeDepartmentManagement.Helper;
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeDepartmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly EmployeRepository repo;

        //constructor
        public EmpController(EmployeRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee([FromForm] EmployeDto dto)
        {
            string name = ImageHelper.SaveImage(dto.Image);
            Employee emp = new Employee()
            {
                EName = dto.EName,
                Salary = dto.Salary,
                DId = dto.DId,
                ImagePath = name
            };
            var res = await this.repo.AddEmployee(emp);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Employee already exists");

        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeDto dto)
        {
            string name = ImageHelper.SaveImage(dto.Image);
            Employee emp = new Employee()
            {
                EName = dto.EName,
                Salary = dto.Salary,
                DId = dto.DId,
                ImagePath = name
            };
            var res = await this.repo.UpdateEmployeeAsync(id, emp);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Employee not found");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var res = await this.repo.GetAllEmployees();
            return Ok(res);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var res = await this.repo.GetEmployeeById(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Employee not found");
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var res = await this.repo.DeleteEmployee(id);
            if (res)
            {
                return Ok("Employee deleted successfully");
            }
            return NotFound("Employee not found");
        }
    }
}
