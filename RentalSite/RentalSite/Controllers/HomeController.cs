using Microsoft.AspNetCore.Mvc;
using RentalSite.Data;
using RentalSite.Models;
using RentalSite.ViewModels;
using System.Diagnostics;

namespace RentalSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var properties = _context.Properties.ToList();

            if (properties.Count == 0)
            {
                ViewBag.Message = "Properties not found.";
                return View();
            }

            return View (properties);
        }

        public IActionResult Detail(int PropertyId)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == PropertyId);

            if (property == null)
            {
                return NotFound();
            }

            var agent = _context.Agents.FirstOrDefault(a => a.AgentId == property.AgentId);

            if (agent == null)
            {
                return NotFound();
            }

            var propertyViewModel = new PropertyViewModel
            {
                PropertyId = property.PropertyId,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                ParkingSpaces = property.ParkingSpaces,
                ERFSize = property.ERFSize,
                About = property.About,
                Province = property.Province,
                City = property.City,
                Suburb = property.Suburb,
                Agent = $"{agent.FirstName} {agent.LastName}"
            };

            return View(propertyViewModel);
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
