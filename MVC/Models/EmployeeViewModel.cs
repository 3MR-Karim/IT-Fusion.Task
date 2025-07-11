namespace MVC.Models
{

        public class EmployeeViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public DateTime JoinDate { get; set; }
            public string Phone { get; set; } = "";
            public string Gender { get; set; } = ""; // "Male" or "Female"
            public decimal Salary { get; set; }
        }
    }


