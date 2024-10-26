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
