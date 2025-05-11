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
    public DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Section-Exhibit relationship with cascade delete
        modelBuilder.Entity<Exhibit>()
            .HasOne(e => e.Section)
            .WithMany(s => s.Exhibits)
            .HasForeignKey(e => e.IdSection)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Ticket-User relationship
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.IdUsers)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Ticket-TicketType relationship
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.TicketType)
            .WithMany(tt => tt.Tickets)
            .HasForeignKey(t => t.IdTicketType)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Ticket-Discount relationship
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Discount)
            .WithMany()
            .HasForeignKey(t => t.IdDiscount)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure Review-Exhibit relationship
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Exhibit)
            .WithMany(e => e.Reviews)
            .HasForeignKey(r => r.IdExhibit)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Review-User relationship
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.IdUsers)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Review-Tour relationship
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Tour)
            .WithMany(t => t.Reviews)
            .HasForeignKey(r => r.IdTour)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure TourBooking-Tour relationship
        modelBuilder.Entity<TourBooking>()
            .HasOne(tb => tb.Tour)
            .WithMany(t => t.TourBookings)
            .HasForeignKey(tb => tb.IdTour)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure TourBooking-User relationship
        modelBuilder.Entity<TourBooking>()
            .HasOne(tb => tb.User)
            .WithMany(u => u.TourBookings)
            .HasForeignKey(tb => tb.IdUsers)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Tour-TourGuide relationship
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.TourGuide)
            .WithMany(g => g.Tours)
            .HasForeignKey(t => t.IdTourGuide)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure TourGuide-User relationship
        modelBuilder.Entity<TourGuide>()
            .HasOne(g => g.User)
            .WithOne(u => u.TourGuides)
            .HasForeignKey<TourGuide>(g => g.IdUsers)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure TourGuideSchedule-TourGuide relationship
        modelBuilder.Entity<TourGuideSchedule>()
            .HasOne(s => s.TourGuide)
            .WithMany(g => g.Schedules)
            .HasForeignKey(s => s.IdTourGuide)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
