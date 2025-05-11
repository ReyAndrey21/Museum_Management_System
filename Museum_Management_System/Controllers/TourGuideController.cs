using Microsoft.AspNetCore.Mvc;

namespace Museum_Management_System.Controllers
{
    public class TourGuideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
