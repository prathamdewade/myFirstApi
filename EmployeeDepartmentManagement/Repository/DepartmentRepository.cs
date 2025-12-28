using EmployeeDepartmentManagement.Data;
using EmployeeDepartmentManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeDepartmentManagement.Repository
{
    public class DepartmentRepository
    {
        private readonly EmpDeptDbContext db;

        public DepartmentRepository(EmpDeptDbContext db)
        {
            this.db = db;
        }
        public async Task<Department> AddDepartment(Department department)
        {
           var exist = await this.db.TblDepartment.FirstOrDefaultAsync(ob=> ob.DName==department.DName);
            if (exist == null)
            {
                var result = await this.db.TblDepartment.AddAsync(department);
                await this.db.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await this.db.TblDepartment.ToListAsync();
        }
        public async Task<Department?> GetDepartmentById(int id)
        {
           Department d= await this.db.TblDepartment.FirstOrDefaultAsync(ob=> ob.DId==id);
            return d;
        }
    }
}
