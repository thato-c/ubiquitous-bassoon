namespace RentalSite.Models
{
    public class Agent
    {
        public int AgentId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string About { get; set; } = string.Empty;

        public int Phone { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
