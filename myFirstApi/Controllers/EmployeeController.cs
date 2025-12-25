using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myFirstApi.Models;
using myFirstApi.Repository.Defination;
using myFirstApi.Services;
using System.Threading.Tasks;

namespace myFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;
        private readonly IEmployeeRepository repo;

        public EmployeeController(IEmployeeService service,IEmployeeRepository repo)
        {
            this.service = service;
            this.repo = repo;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Employee emp)
        {
            if (ModelState.IsValid)
            {
                bool res = await service.RegisterAsync(emp);
                if (res)
                {
                    return Ok(new { Message = "Data Added SuccessFully", Status = res, Data = emp.Name });
                }
                return BadRequest(new { Message = "Not Added", Status = res });
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
           var res=await repo.GetAllEmployeeAsync();
            return res.Count > 0 ? Ok(res) : NotFound("Data Not Found"); 
                  
        }
        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await repo.GetEmployeeByIdAsync(id);
            return res!=null ? Ok(new { Message ="Data Found", Status =true,Data=res}) : NotFound("Data Not Found");

        }


        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           bool res= await  service.DeleteEmployeeAsync(id);
            return res ? Ok(new { Message = "Data Deleted", Status = true, Data = id }) : BadRequest("data not Deleted");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id , [FromBody] Employee emp)
        {
           bool res= await service.ModifyEmployeeAsync(id, emp);
            return res ? Ok(new { Message = "Data Updated", Status = true, Data = emp.Name }) : BadRequest("data not Updated");
        }
           
    }
}
