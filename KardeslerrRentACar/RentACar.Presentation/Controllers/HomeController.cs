using Microsoft.AspNetCore.Mvc;
using RentACar.Presentation.Models;
using System.Diagnostics;
using RentACar.Application;
using RentACar.DTOs.Vehicle;

namespace RentACar.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VehicleService _vehicleService;
        public HomeController(ILogger<HomeController> logger, VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ListVehicleDTO listVehicleDto = _vehicleService.GetVehicles();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
