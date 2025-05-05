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
    [DataType(DataType.Time)]
    public TimeOnly OpeningHour { get; set; }
    [Column("closing_hour")]
    [DataType(DataType.DateTime)]
    public TimeOnly ClosingHour { get; set; }
}
