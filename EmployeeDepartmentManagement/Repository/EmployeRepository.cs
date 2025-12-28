using EmployeeDepartmentManagement.Data;
using EmployeeDepartmentManagement.Helper;
using EmployeeDepartmentManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace EmployeeDepartmentManagement.Repository
{
    public class EmployeRepository
    {
        private readonly EmpDeptDbContext db;

        public EmployeRepository(EmpDeptDbContext db)
        {
            this.db = db;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
           Employee emp=await this.db.TblEmployee.FirstOrDefaultAsync(ob => ob.EName == employee.EName);
            if (emp != null)
            {
                return null;
            }
            EntityEntry<Employee> res=await this.db.TblEmployee.AddAsync(employee);
            await this.db.SaveChangesAsync();
            return res.Entity;
        }
        public async Task<Employee> UpdateEmployeeAsync(int id ,Employee emp)
        {
            Employee exist = await this.db.TblEmployee.FirstOrDefaultAsync(ob =>ob.EId== id);
            if (exist != null)
            {
                ImageHelper.DeleteImage(exist.ImagePath);
                exist.EName = emp.EName;
                exist.Salary = emp.Salary;
                exist.ImagePath = emp.ImagePath;
                exist.DId = emp.DId;
                await this.db.SaveChangesAsync();
                return exist;
            }
            return null;

        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await this.db.TblEmployee.Include(e=> e.Department).ToListAsync();
        }
        public async Task<Employee?> GetEmployeeById(int id)
        {
            Employee e = await this.db.TblEmployee.Include(e=> e.Department).FirstOrDefaultAsync(ob => ob.EId == id);
            return e;
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            Employee? emp = await this.db.TblEmployee.FirstOrDefaultAsync(ob => ob.EId == id);
            if (emp != null)
            {
                ImageHelper.DeleteImage(emp.ImagePath);
                this.db.TblEmployee.Remove(emp);
                await this.db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
