namespace EmployeeDepartmentManagement.Dto
{
    public class EmployeDto
    {
        public string EName { get; set; }
        public decimal Salary { get; set; }
        public IFormFile Image { get; set; }
       
        public int DId { get; set; }
    }
}
