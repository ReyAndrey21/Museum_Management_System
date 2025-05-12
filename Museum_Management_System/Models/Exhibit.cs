namespace Museum_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Exhibit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_exhibit")]
    public int IdExhibit { get; set; }
    [Column("name_exhibit")]
    public string? NameExhibit { get; set; }
    [Column("description_exhibit")]
    public string? Description { get; set; }
    [Column("historical_period")]
    public string? HistoricalPeriod { get; set; }
    [Column("category_exhibit")]
    public string? CategoryExhibit { get; set; }
    [Column("image_exhibit")]
    public string? ImageExhibit { get; set; }
    [NotMapped]
    public IFormFile? ImageExhibitFile { get; set; }
    [ForeignKey("Section")]
    [Column("id_section")]
    public int? IdSection { get; set; }
    public Section? Section { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
