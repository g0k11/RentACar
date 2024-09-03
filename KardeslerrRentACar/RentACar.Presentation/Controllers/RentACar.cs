using Microsoft.AspNetCore.Mvc;

namespace RentACar.Presentation.Controllers
{
    public class RentACar : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
