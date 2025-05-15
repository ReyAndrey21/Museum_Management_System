namespace Museum_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Tour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour")]
    public int IdTour { get; set; }
    [Column("title")]
    public string? Title { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("available_spots")]
    public int AvailableSpots { get; set; }
    [Column("duration")]
    public int Duration { get; set; }
    [Column("date_tour")]
    [DataType(DataType.Date)]
    public DateOnly DateTour { get; set; }
    [Column("hour_tour")]
    [DataType(DataType.Time)]
    public TimeOnly HourTour { get; set; }
    [ForeignKey("TourGuide")]
    [Column("id_tour_guide")]
    public int IdTourGuide { get; set; }
    public TourGuide? TourGuide { get; set; }
    public ICollection<TourBooking>? TourBookings { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
