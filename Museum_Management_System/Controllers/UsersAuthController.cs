using Microsoft.AspNetCore.Mvc;
using Museum_Management_System.Data;
using Museum_Management_System.Models;

namespace Museum_Management_System.Controllers
{
    public class UsersAuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UsersAuthController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public IActionResult ViewProfile()
        {
            var id = HttpContext.Session.GetInt32("IdUsers");
            if (id == null)
                return RedirectToAction("Login");

            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return View(user);
        }


        [HttpPost]
        public IActionResult UpdateProfile(Users model)
        {
            
            ModelState.Remove(nameof(model.Password));

            if (!ModelState.IsValid)
                return View("ViewProfile", model);

            var user = _context.Users.Find(model.IdUsers);
            if (user == null) return NotFound();

           
            user.Username = model.Username;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }
            user.Role = model.Role;

            
            if (model.ProfilePictureFile != null && model.ProfilePictureFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "profile_picture");
                Directory.CreateDirectory(uploads);

                var fileName = Path.GetFileName(model.ProfilePictureFile.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                model.ProfilePictureFile.CopyTo(stream);

                user.ProfilePicture = fileName;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(ViewProfile));
        }


    }
}
