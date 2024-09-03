using Microsoft.AspNetCore.Mvc;

namespace RentACar.Presentation.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
