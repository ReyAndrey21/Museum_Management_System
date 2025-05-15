using Microsoft.AspNetCore.Mvc;
using Museum_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Models;

namespace Museum_Management_System.Controllers
{
    public class VisitorController : Controller
    {
        private readonly AppDbContext _context;

        public VisitorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult DashboardVisitor()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ViewMuseumSchedule()
        {
            var schedule = _context.MuseumSchedules.ToList();
            return View(schedule);
        }

        public IActionResult ViewSections()
        {
            var sections = _context.Sections
                .Include(s => s.Exhibits)
                .ToList();

            return View(sections);
        }

        public IActionResult ViewExhibitsFromSections(int id)
        {
            var section = _context.Sections
                .Include(s => s.Exhibits)
                .FirstOrDefault(s => s.IdSection == id);

            if (section == null)
                return NotFound();

            return View("ExhibitsFromSections", section);
        }

        public IActionResult BuyTicket()
        {
            ViewBag.TicketTypes = _context.TicketTypes.ToList();
            ViewBag.Discounts = _context.Discounts.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BuyTicket(Ticket ticket)
        {
            var userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null) return RedirectToAction("Login", "UsersAuth");

            ticket.IdUsers = userId.Value;
            ticket.PurchaseDate = DateOnly.FromDateTime(DateTime.Today);

            var basePrice = _context.TicketTypes.Find(ticket.IdTicketType)?.BasePrice ?? 0;
            var discount = _context.Discounts.Find(ticket.IdDiscount);
            ticket.FinalPrice = discount != null
                ? basePrice * (1 - discount.PercentageDiscount / 100.0)
                : basePrice;

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Biletul a fost achiziționat cu succes!";
            return RedirectToAction("BuyTicket");
        }

        public IActionResult TicketHistory()
        {
            var userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null) return RedirectToAction("Login", "UsersAuth");

            var tickets = _context.Tickets
                .Include(t => t.TicketType)
                .Include(t => t.Discount)
                .Where(t => t.IdUsers == userId)
                .ToList();

            return View(tickets);
        }

        public IActionResult AvailableTours()
        {
            var tours = _context.Tours
                .Include(t => t.TourGuide)
                .ThenInclude(g => g.User)
                .ToList();

            return View(tours);
        }

        public IActionResult BookTour(int id)
        {
            var tour = _context.Tours
                .Include(t => t.TourGuide)
                .ThenInclude(g => g.User)
                .FirstOrDefault(t => t.IdTour == id);

            if (tour == null) return NotFound();
            return View(tour);
        }

        [HttpPost]
        public IActionResult BookTour(int id, int numberOfTickets)
        {
            var userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null) return RedirectToAction("Login", "UsersAuth");

            var tour = _context.Tours.FirstOrDefault(t => t.IdTour == id);
            if (tour == null || tour.AvailableSpots < numberOfTickets) return BadRequest();

            var booking = new TourBooking
            {
                IdTour = id,
                IdUsers = userId.Value,
                NumberTickets = numberOfTickets
            };

            tour.AvailableSpots -= numberOfTickets;
            _context.TourBookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("AvailableTours");
        }

        public IActionResult ViewProfileVisitor()
        {
            var id = HttpContext.Session.GetInt32("IdUsers");
            if (id == null)
                return RedirectToAction("Login", "UsersAuth");

            var user = _context.Users
                .Include(u => u.Tickets)
                    .ThenInclude(t => t.TicketType)
                .Include(u => u.Tickets)
                    .ThenInclude(t => t.Discount)
                .Include(u => u.TourBookings)
                    .ThenInclude(tb => tb.Tour)
                .FirstOrDefault(u => u.IdUsers == id);

            if (user == null)
                return NotFound();

            return View(user); 
        }

        public IActionResult DeleteAccount()
        {
            return RedirectToAction("DeleteAccount", "UsersAuth");
        }

        [HttpPost]
        public IActionResult CancelTourBooking(int id)
        {
            var userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null) return RedirectToAction("Login", "UsersAuth");

            var booking = _context.TourBookings.FirstOrDefault(b => b.IdTourBooking == id && b.IdUsers == userId);
            if (booking != null)
            {
                // Oferă înapoi locurile la tur
                var tour = _context.Tours.FirstOrDefault(t => t.IdTour == booking.IdTour);
                if (tour != null)
                {
                    tour.AvailableSpots += booking.NumberTickets;
                }
                _context.TourBookings.Remove(booking);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Rezervarea a fost anulată cu succes!";
            }
            else
            {
                TempData["ErrorMessage"] = "Rezervarea nu a putut fi găsită sau nu vă aparține.";
            }
            return RedirectToAction("ViewProfileVisitor");
        }

        [HttpPost]
        public IActionResult AddExhibitReview(int idExhibit, int rating, string text)
        {
            var userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null) return RedirectToAction("Login", "UsersAuth");

            var review = new Review
            {
                IdExhibit = idExhibit,
                IdUsers = userId.Value,
                Rating = rating,
                Comment = text,
                DateReview = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Reviews.Add(review);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Review-ul a fost adăugat!";
            // Redirect la secțiunea curentă pentru a vedea review-ul nou
            var exhibit = _context.Exhibits.FirstOrDefault(e => e.IdExhibit == idExhibit);
            if (exhibit != null)
                return RedirectToAction("ViewExhibitsFromSections", new { id = exhibit.IdSection });
            return RedirectToAction("ViewSections");
        }

        public IActionResult ViewFAQ()
        {
            var faqs = _context.Faqs.ToList();
            return View(faqs);
        }
    }
}
