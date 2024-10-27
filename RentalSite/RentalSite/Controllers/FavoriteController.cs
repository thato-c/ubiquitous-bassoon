using Microsoft.AspNetCore.Mvc;
using RentalSite.Data;
using RentalSite.Models;
using System.Security.Claims;

namespace RentalSite.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var favorites = _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.PropertyId)
                .ToList();

            var properties = _context.Properties
                .Where(p => favorites.Contains(p.PropertyId))
                .ToList();

            if (properties == null || !properties.Any())
            {
                ViewBag.Message = "No favorite properties were found.";
                return View();
            }

            return View(properties);
        }

        public IActionResult Add(int PropertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var existingFavorite = _context.Favorites
                .FirstOrDefault(w => w.PropertyId == PropertyId && w.UserId == userId);

            if (existingFavorite != null)
            {
                TempData["Message"] = "This property is already in your wish list.";
                return RedirectToAction("Index", "Home");
            }

            var favorite = new Favorites
            {
                PropertyId = PropertyId,
                UserId = userId
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            TempData["Message"] = "Property added to your wishlist successfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
