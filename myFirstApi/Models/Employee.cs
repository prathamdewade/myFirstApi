using System.ComponentModel.DataAnnotations;

namespace myFirstApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50, ErrorMessage ="Name can't be longer than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(50,ErrorMessage = "Password can't be longer than 50 characters")]
        public string Password { get; set; }
    }
}
