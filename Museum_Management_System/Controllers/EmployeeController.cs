using Microsoft.AspNetCore.Mvc;
using Museum_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Museum_Management_System.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ListEmployees()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }
    }
}
