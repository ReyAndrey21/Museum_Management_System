using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Models;
namespace Museum_Management_System.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Users> Users { get; set; }
    public DbSet<TourGuide> TourGuides { get; set; }
    public DbSet<MuseumSchedule> MuseumSchedules { get; set; }
    public DbSet<TourGuideSchedule> TourGuideSchedules { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<Exhibit> Exhibits { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourBooking> TourBookings { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

}
