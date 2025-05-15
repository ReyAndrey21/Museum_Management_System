using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;

public class TourGuide
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour_guide")]
    public int IdTourGuide { get; set; }
    [Column("status")]
    public string? Status { get; set; }
    [Column("foreign_languages")]
    public string? ForeignLanguages { get; set; }
    [ForeignKey("User")]
    [Column("id_users")]
    public int IdUsers { get; set; }
    public Users? User { get; set; }
    public ICollection<TourGuideSchedule>? Schedules { get; set; }
    public ICollection<Tour>? Tours { get; set; }
}
