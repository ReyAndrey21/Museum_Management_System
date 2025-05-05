using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;
[Table("FAQ")]
public class Faq
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_faq")]
    public int IdFAQ { get; set; }
    [Column("question")]
    public string? Question { get; set; }
    [Column("answer")]
    public string? Answer { get; set; }
}
