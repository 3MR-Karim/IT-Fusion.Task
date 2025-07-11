namespace WebApplication1.Models
{

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime JoinDate { get; set; }
        public string Phone { get; set; } = "";
        public string Gender { get; set; } = "";
        public decimal Salary { get; set; }
    }
}
