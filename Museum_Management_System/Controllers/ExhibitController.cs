using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Data;

namespace Museum_Management_System.Controllers
{
    public class ExhibitController : Controller
    {
        private readonly AppDbContext _context;

        public ExhibitController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ListExhibits()
        {
            var exhibits = _context.Exhibits
                .Include(e => e.Section)
                .ToList();
            return View(exhibits);
        }

    
    }
}
