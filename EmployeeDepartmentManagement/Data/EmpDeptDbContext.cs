using EmployeeDepartmentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartmentManagement.Data
{
    public class EmpDeptDbContext : DbContext
    {
       public EmpDeptDbContext(DbContextOptions<EmpDeptDbContext> options) : base(options)
        {
        }
        public DbSet<Department> TblDepartment { get; set; }
        public DbSet<Employee> TblEmployee { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                 .HasMany(d => d.Employees).
                 WithOne(e => e.Department).
                 HasForeignKey(e => e.DId);

        }
    }
}
