using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Museum_Management_System.Models;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_users")]
    public int IdUsers { get; set; }
    [Column("username")]
    public string? Username { get; set; }
    [Column("email")]
    [Required]
    public string? Email { get; set; }
    [Column("password")]
    [Required]
    public string? Password { get; set; }
    [Column("first_name")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("profile_picture")]
    public string? ProfilePicture { get; set; }
    [NotMapped]
    public IFormFile? ProfilePictureFile { get; set; }

    [Column("role")]
    public string? Role { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<TourBooking>? TourBookings { get; set; }
    public TourGuide? TourGuides { get; set; }
}
