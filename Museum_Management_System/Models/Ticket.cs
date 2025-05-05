using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;

public class Ticket

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_ticket")]
    public int IdTicket { get; set; }
    [Column("price")]
    public double Price { get; set; }
    [Column("purchase_date")]
    [DataType(DataType.Date)]
    public DateOnly PurchaseDate { get; set; }
    [Column("ticket_type")]
    public string? TicketType { get; set; }
    [ForeignKey("Users")]
    [Column("id_users")]

    public int IdUsers { get; set; }
    public Users? User { get; set; }

    [ForeignKey("Discount")]
    [Column("id_discount")]
    public int? IdDiscount { get; set; }
    public Discount? Discount { get; set; }
}
