using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;

public class Ticket

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_ticket")]
    public int IdTicket { get; set; }
    [Column("purchase_date")]
    [DataType(DataType.Date)]
    public DateOnly PurchaseDate { get; set; }
    [Column("final_price")]
    public double FinalPrice { get; set; }
    [ForeignKey("TicketType")]
    [Column("id_ticket_type")]
    public int IdTicketType { get; set; }
    public TicketType? TicketType { get; set; }
    [ForeignKey("Users")]
    [Column("id_users")]
    public int IdUsers { get; set; }
    public Users? User { get; set; }

    [ForeignKey("Discount")]
    [Column("id_discount")]
    public int? IdDiscount { get; set; }
    public Discount? Discount { get; set; }
}
