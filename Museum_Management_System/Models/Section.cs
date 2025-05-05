using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum_Management_System.Models;

public class Section
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_section")]
    public int IdSection { get; set; }
    [Column("name_section")]
    public string? NameSection { get; set; }
    [Column("description_section")]
    public string? Description { get; set; }
    [Column("image_section")]
    public string? ImageSection { get; set; }
    [NotMapped]
    public IFormFile? ImageSectionFile { get; set; }

    public ICollection<Exhibit>? Exhibits { get; set; }
}
