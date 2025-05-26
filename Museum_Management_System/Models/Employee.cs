using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_employee")]
    public int Id { get; set; }
    [Column("first_name")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("hire_date")]
    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }
    [Column("salary")]
    public double Salary { get; set; }
    [Column("position")]
    public string? Position { get; set; }
}
