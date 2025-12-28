using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeDepartmentManagement.Models
{
    public class Department
    {
        [Key]
        public int DId { get; set; }
        public string DName { get; set; }
        [JsonIgnore]
        //navigation property
        public List<Employee> Employees { get; set; }=new List<Employee>();
    }

    public class Employee
    {
        [Key]
        public int EId { get; set; }
        public string EName { get; set; }
        public decimal Salary { get; set; }
        public string ImagePath { get; set; }
        //foreign key 
        //here we are defining DId as foreign key referencing Department
        //[ForeignKey("Department")]
        public int DId { get; set; }
        //navigation property
        public Department Department { get; set; }

    }
}
