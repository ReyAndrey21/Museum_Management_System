namespace Museum_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Discount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_discount")]
    public int IdDiscount { get; set; }
    [Column("percentage_discout")]
    public double PercentageDiscount { get; set; }
    [Column("beneficiary_category")]
    public string? BeneficiaryCategory { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}
