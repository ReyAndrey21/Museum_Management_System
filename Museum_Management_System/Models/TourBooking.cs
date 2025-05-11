namespace Museum_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TourBooking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour_booking")]
    public int IdTourBooking { get; set; }
    [Column("number_tickets")]
    public int NumberTickets { get; set; }
    [ForeignKey("User")]
    [Column("id_users")]

    public int IdUsers { get; set; }
    public Users? User { get; set; }
    [ForeignKey("Tour")]
    [Column("id_tour")]
    public int IdTour { get; set; }
    public Tour? Tour { get; set; }
}
