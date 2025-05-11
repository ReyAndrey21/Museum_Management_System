using Microsoft.AspNetCore.Mvc;

namespace Museum_Management_System.Controllers
{
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
