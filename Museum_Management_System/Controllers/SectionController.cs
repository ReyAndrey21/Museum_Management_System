using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Data;

namespace Museum_Management_System.Controllers
{
    public class SectionController : Controller
    {
        private readonly AppDbContext _context;

        public SectionController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult ListSections()
        {
            var sections = _context.Sections.ToList();
            return View(sections);
        }

        
        public IActionResult DetailsSections(int id)
        {
            var section = _context.Sections
                .Include(s => s.Exhibits)
                .FirstOrDefault(s => s.IdSection == id);

            if (section == null)
                return NotFound();

            return View(section);
        }
    }
}
