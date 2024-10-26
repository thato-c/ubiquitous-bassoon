using Microsoft.AspNetCore.Mvc;
using RentalSite.Models;

namespace RentalSite.Controllers
{
    public class AgentController : Controller
    {
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
            if (_agents.Count == 0)
            {
                ViewBag.Message = "Agents not found.";
                return View();
            }

            return View(_agents);
        }

        public IActionResult Detail(int AgentId)
        {
            var agent = _agents.FirstOrDefault(a => a.AgentId == AgentId);

            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }
    }
}
