using Microsoft.AspNetCore.Identity;

namespace RentalSite.Models
{
    public class Favorites
    {
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public string  UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
