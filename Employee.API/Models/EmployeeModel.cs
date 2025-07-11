using System.ComponentModel.DataAnnotations;

public class EmployeeModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime JoinDate { get; set; }

    [Phone]
    public string Phone { get; set; }

    [Required]
    public string Gender { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }
}
