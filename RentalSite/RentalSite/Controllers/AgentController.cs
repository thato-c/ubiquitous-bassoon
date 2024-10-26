using Microsoft.AspNetCore.Mvc;
using RentalSite.Data;
using RentalSite.Models;

namespace RentalSite.Controllers
{
    public class AgentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AgentController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public IActionResult Index()
        {
            var agents = _context.Agents.ToList();

            if (agents.Count == 0)
            {
                ViewBag.Message = "Agents not found.";
                return View();
            }

            return View(agents);
        }

        public IActionResult Detail(int AgentId)
        {
            var agent = _context.Agents.FirstOrDefault(a => a.AgentId == AgentId);

            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }
    }
}
