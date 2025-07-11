using System.ComponentModel.DataAnnotations;

namespace Employee.MVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive")]
        public decimal Salary { get; set; }
    }
}
