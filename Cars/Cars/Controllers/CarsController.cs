using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
