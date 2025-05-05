using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museum_Management_System.Models;

public class TourGuideSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour_guide_schedule")]
    public int IdTourGuideSchedule { get; set; }
    [Column("day_of_week")]
    public string? DayOfWeek { get; set; }
    [Column("start_hour")]
    public TimeSpan StartHour { get; set; }
    [Column("end_hour")]
    public TimeSpan EndHour { get; set; }
    [ForeignKey("TourGuide")]
    [Column("id_tour_guide")]

    public int IdTourGuide { get; set; }
    public TourGuide? TourGuide { get; set; }
}
