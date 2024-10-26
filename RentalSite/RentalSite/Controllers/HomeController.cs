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

        // Sample Data
        private List<Property> _properties = new List<Property>
        {
            new Property {
                PropertyId = 1,
                Bedrooms = 1,
                Bathrooms = 1,
                ParkingSpaces = 1,
                ERFSize = 1500,
                About = "This is the first property",
                Province = "Gauteng",
                City = "Johannesburg",
                Suburb = "Bassonia",
                AgentId = 1
            },

            new Property {
                PropertyId = 2,
                Bedrooms = 2,
                Bathrooms = 2,
                ParkingSpaces = 2,
                ERFSize = 1300,
                About = "This is the second property",
                Province = "KwaZulu Natal",
                City = "Durban",
                Suburb = "Soweto",
                AgentId = 2
            },

            new Property {
                PropertyId = 3,
                Bedrooms = 3,
                Bathrooms = 3,
                ParkingSpaces = 3,
                ERFSize = 1200,
                About = "This is the third property",
                Province = "Eastern Cape",
                City = "Cape Town",
                Suburb = "Heaven",
                AgentId = 3
            },
        };

        private List<Agent> _agents = new List<Agent> 
        {
            new Agent
            {
                AgentId = 1,
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Phone = 0907994742,
                About = "The first Agent"
            },

            new Agent
            {
                AgentId = 2,
                FirstName = "Test2 First Name",
                LastName = "Test2 Last Name",
                Phone = 0907994742,
                About = "The second Agent"
            },

            new Agent
            {
                AgentId = 3,
                FirstName = "Test3 First Name",
                LastName = "Test3 Last Name",
                Phone = 0907994742,
                About = "The third Agent"
            }
        };

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
