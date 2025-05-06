using Microsoft.AspNetCore.Mvc;
using Museum_Management_System.Data;
using Museum_Management_System.Models;

namespace Museum_Management_System.Controllers
{
    public class UsersAuthController : Controller
    {
        private readonly AppDbContext _context;

        public UsersAuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Users user)
        {
            if (_context.Users.Any(u => u.Email == user.Email && u.Role == user.Role))
            {
                ModelState.AddModelError("Email", "This email is already used for this role.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("IdUsers", user.IdUsers);
                if (user.Role == "admin")
                    return RedirectToAction("DashboardAdmin", "Admin");
                else if (user.Role == "visitor")
                    return RedirectToAction("DashboardVisitor", "Visitor");
                else if (user.Role == "tour guide")
                    return RedirectToAction("DashboardTourGuide", "TourGuide");
            }

            ViewBag.Error = "Incorrect email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
