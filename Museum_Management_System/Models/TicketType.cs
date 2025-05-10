namespace Museum_Management_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TicketType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_ticket_type")]
    public int IdTicketType { get; set; }

    [Column("type_name")]
    public string? TypeName { get; set; }

    [Column("base_price")]
    public double BasePrice { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}
