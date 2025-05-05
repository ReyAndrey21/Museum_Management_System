using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;

public class MuseumSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_museum_schedule")]
    public int IdMuseumSchedule { get; set; }
    [Column("day_of_week")]
    public string? DayOfWeek { get; set; }
    [Column("opening_hour")]
    public TimeSpan OpeningHour { get; set; }
    [Column("closing_hour")]
    public TimeSpan ClosingHour { get; set; }
}
