using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Data;
using Museum_Management_System.Models;

namespace Museum_Management_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        
        public IActionResult DashboardAdmin()
        {
            ViewBag.UserCount = _context.Users.Count();
            ViewBag.EmployeeCount = _context.Employees.Count();
            ViewBag.ExhibitCount = _context.Exhibits.Count();
            ViewBag.SectionCount = _context.Sections.Count();
            ViewBag.ReviewCount = _context.Reviews.Count();
            ViewBag.FaqCount = _context.Faqs.Count();
            ViewBag.TicketCount = _context.Tickets.Count();
            return View();
        }

        public IActionResult Logout() => RedirectToAction("Logout", "UsersAuth");

        
        public IActionResult ViewUsers() => View(_context.Users.ToList());

        
        public IActionResult IndexEmployees() => View(_context.Employees.ToList());

        public IActionResult AddEmployee() => View();
        [HttpPost]
        public IActionResult AddEmployee(Employee e)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(e);
                _context.SaveChanges();
                return RedirectToAction("IndexEmployees");
            }
            return View(e);
        }

        public IActionResult UpdateEmployee(int id)
        {
            return View(_context.Employees.Find(id));
        }

        [HttpPost]
        public IActionResult UpdateEmployee(Employee e)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(e);
                _context.SaveChanges();
                return RedirectToAction("IndexEmployees");
            }
            return View(e);
        }

        public IActionResult DeleteEmployee(int id) => View(_context.Employees.Find(id));
        [HttpPost]
        public IActionResult ConfirmDeleteEmployee(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return RedirectToAction("IndexEmployees");
        }

        public IActionResult IndexSections() => View(_context.Sections.ToList());

        public IActionResult AddSection() => View();
        [HttpPost]
        public IActionResult AddSection(Section s)
        {
            if (ModelState.IsValid)
            {
                if (s.ImageSectionFile != null)
                {
                    var fileName = Path.GetFileName(s.ImageSectionFile.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images/images_sections", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    s.ImageSectionFile.CopyTo(stream);
                    s.ImageSection = fileName;
                }

                _context.Sections.Add(s);
                _context.SaveChanges();
                return RedirectToAction("IndexSections");
            }
            return View(s);
        }

        public IActionResult UpdateSection(int id) => View(_context.Sections.Find(id));
        [HttpPost]
        public IActionResult UpdateSection(Section s)
        {
            if (ModelState.IsValid)
            {
                _context.Sections.Update(s);
                _context.SaveChanges();
                return RedirectToAction("IndexSections");
            }
            return View(s);
        }

        public IActionResult DeleteSection(int id) => View(_context.Sections.Find(id));
        [HttpPost]
        public IActionResult ConfirmDeleteSection(int id)
        {
            var s = _context.Sections.Find(id);
            if (s != null)
            {
                _context.Sections.Remove(s);
                _context.SaveChanges();
            }
            return RedirectToAction("IndexSections");
        }

       
        public IActionResult IndexExhibits() =>
            View(_context.Exhibits.Include(e => e.Section).ToList());

        public IActionResult AddExhibit() => View();
        [HttpPost]
        public IActionResult AddExhibit(Exhibit e)
        {
            if (ModelState.IsValid)
            {
                if (e.ImageExhibitFile != null)
                {
                    var fileName = Path.GetFileName(e.ImageExhibitFile.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images/images_exhibits", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    e.ImageExhibitFile.CopyTo(stream);
                    e.ImageExhibit = fileName;
                }

                _context.Exhibits.Add(e);
                _context.SaveChanges();
                return RedirectToAction("IndexExhibits");
            }
            return View(e);
        }

        public IActionResult UpdateExhibit(int id) => View(_context.Exhibits.Find(id));
        [HttpPost]
        public IActionResult UpdateExhibit(Exhibit e)
        {
            if (ModelState.IsValid)
            {
                _context.Exhibits.Update(e);
                _context.SaveChanges();
                return RedirectToAction("IndexExhibits");
            }
            return View(e);
        }

        public IActionResult DeleteExhibit(int id) => View(_context.Exhibits.Find(id));
        [HttpPost]
        public IActionResult ConfirmDeleteExhibit(int id)
        {
            var e = _context.Exhibits.Find(id);
            if (e != null)
            {
                _context.Exhibits.Remove(e);
                _context.SaveChanges();
            }
            return RedirectToAction("IndexExhibits");
        }

        public IActionResult SearchExhibit(string? name)
        {
            var result = _context.Exhibits
                .Include(e => e.Section).Where(e => e.NameExhibit.Contains(name ?? "")).ToList();
            return View("IndexExhibits", result);
        }

        
        public IActionResult IndexTicketTypes() => View(_context.TicketTypes.ToList());

        public IActionResult AddTicketType() => View();
        [HttpPost]
        public IActionResult AddTicketType(TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _context.TicketTypes.Add(ticketType);
                _context.SaveChanges();
                return RedirectToAction("IndexTicketTypes");
            }
            return View(ticketType);
        }

        public IActionResult UpdateTicketType(int id) => View(_context.TicketTypes.Find(id));
        [HttpPost]
        public IActionResult UpdateTicketType(TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _context.TicketTypes.Update(ticketType);
                _context.SaveChanges();
                return RedirectToAction("IndexTicketTypes");
            }
            return View(ticketType);
        }

        public IActionResult DeleteTicketType(int id) => View(_context.TicketTypes.Find(id));
        [HttpPost]
        public IActionResult ConfirmDeleteTicketType(int id)
        {
            var ticketType = _context.TicketTypes.Find(id);
            if (ticketType != null)
            {
                _context.TicketTypes.Remove(ticketType);
                _context.SaveChanges();
            }
            return RedirectToAction("IndexTicketTypes");
        }



        // REVIEWS
        public IActionResult ViewAllReviews() =>
            View(_context.Reviews.Include(r => r.User).Include(r => r.Exhibit).ToList());

        
        public IActionResult IndexMuseumSchedule()
        {
            return View(_context.MuseumSchedules.ToList());
        }

        public IActionResult AddMuseumSchedule() => View();

        [HttpPost]
        public IActionResult AddMuseumSchedule(MuseumSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.MuseumSchedules.Add(schedule);
                _context.SaveChanges();
                return RedirectToAction("IndexMuseumSchedule");
            }
            return View(schedule);
        }

        public IActionResult UpdateMuseumSchedule(int id)
        {
            var schedule = _context.MuseumSchedules.Find(id);
            return View(schedule);
        }

        [HttpPost]
        public IActionResult UpdateMuseumSchedule(MuseumSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.MuseumSchedules.Update(schedule);
                _context.SaveChanges();
                return RedirectToAction("IndexMuseumSchedule");
            }
            return View(schedule);
        }


        
        public IActionResult ManageDiscounts() => View(_context.Discounts.ToList());

        
        public IActionResult ListFAQ() => View(_context.Faqs.ToList());

        [HttpPost]
        public IActionResult AddFAQ(Faq faq)
        {
            if (ModelState.IsValid)
            {
                _context.Faqs.Add(faq);
                _context.SaveChanges();
                return RedirectToAction("ListFAQ");
            }
            return View("ListFAQ");
        }

        public IActionResult DeleteFAQ(int id)
        {
            var faq = _context.Faqs.Find(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                _context.SaveChanges();
            }
            return RedirectToAction("ListFAQ");
        }

        
        public IActionResult Profile()
        {
            return RedirectToAction("ViewProfile", "UsersAuth");
        }
    }
}
