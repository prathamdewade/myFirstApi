using myFirstApi.Helper;
using myFirstApi.Models;
using myFirstApi.Repository.Defination;

namespace myFirstApi.Services
{


    public interface IEmployeeService
    {
        Task<bool> RegisterAsync(Employee emp);
        Task<Employee> LoginAsync(string email, string password);
        Task<bool> ModifyEmployeeAsync(int id, Employee emp);
        Task<bool> DeleteEmployeeAsync(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
           Employee emp=await repo.GetEmployeeByIdAsync(id);
            if (emp != null)
            {
              return await  repo.DeleteEmployeeAsync(id);
            }
            return false;
        }

        public async Task<Employee> LoginAsync(string email, string password)
        {
            Employee emp = await repo.GetEmployeeByEmailAsync(email);
            if (emp != null)
            {
                bool res=PasswordEncoder.Verification(password, emp.Password);
                if (!res) {
                    Console.WriteLine("login Fail you Enter Wrong  password ");
                    return null;
                }
                return emp;

            }
            return null;
        }

        public async Task<bool> ModifyEmployeeAsync(int id, Employee emp)
        {
            Employee exist=await repo.GetEmployeeByIdAsync(id);
            if (exist != null)
            {
                exist.Id = id;
                exist.Name = emp.Name;
                exist.Email = emp.Email;
                exist.Password =PasswordEncoder.Encode(emp.Password);
              return  await repo.UpdateEmployeeAsync(exist);
            }
            return false;
        }

        public async Task<bool> RegisterAsync(Employee emp)
        {
            Employee exist = await repo.GetEmployeeByEmailAsync(emp.Email);
            if(exist != null)
            {
                return false;
            }
            emp.Password=PasswordEncoder.Encode(emp.Password);
           return await repo.AddEmployeeAsync(emp);
        }
    }
}
