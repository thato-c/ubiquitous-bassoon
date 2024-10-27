using Microsoft.AspNetCore.Mvc;
using RentalSite.Data;
using RentalSite.Models;
using System.Security.Claims;

namespace RentalSite.Controllers
{
    public class WishListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishListController(ApplicationDbContext context)
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

            var wishList = _context.WishList.Where(w => w.User.Id == userId).Select(w => w.PropertyId).ToList();

            var properties = _context.Properties.Where(p => wishList.Contains(p.PropertyId)).ToList();

            if (properties == null || !properties.Any())
            {
                ViewBag.Message = "No properties in your wish list were found.";
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

            var existingWish = _context.WishList
                .FirstOrDefault(w => w.PropertyId == PropertyId && w.UserId == userId);

            if (existingWish != null)
            {
                TempData["Message"] = "This property is already in your wish list.";
                return RedirectToAction("Index", "Home");
            }

            var wish = new WishList
            {
                PropertyId = PropertyId,
                UserId = userId
            };

            _context.WishList.Add(wish);
            _context.SaveChanges();

            TempData["Message"] = "Property added to your wishlist successfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
