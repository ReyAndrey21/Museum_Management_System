using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museum_Management_System.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_review")]
    public int IdReview { get; set; }
    [Column("rating")]
    [Range(1, 5)]
    public int Rating { get; set; }
    [Column("comment")]
    public string? Comment { get; set; }
    [Column("date_review")]
    [DataType(DataType.Date)]
    public DateOnly DateReview { get; set; }
    [ForeignKey("User")]
    [Column("id_users")]
    public int IdUsers { get; set; }
    public Users? User { get; set; }
    [ForeignKey("Exhibit")]
    [Column("id_exhibit")]

    public int? IdExhibit { get; set; }
    public Exhibit? Exhibit { get; set; }
    [ForeignKey("Tour")]
    [Column("id_tour")]
    public int? IdTour { get; set; }
    public Tour? Tour { get; set; }
}
