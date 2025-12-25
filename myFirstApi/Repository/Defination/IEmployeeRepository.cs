using myFirstApi.Models;

namespace myFirstApi.Repository.Defination
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<bool> AddEmployeeAsync(Employee emp);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<bool> UpdateEmployeeAsync(Employee emp);
        Task<bool> DeleteEmployeeAsync(int id);
    }

}
