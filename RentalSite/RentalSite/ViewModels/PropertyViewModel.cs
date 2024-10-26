﻿namespace RentalSite.ViewModels
{
    public class PropertyViewModel
    {
        public int PropertyId { get; set; }

        public string Province { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Suburb { get; set; } = string.Empty;

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int ParkingSpaces { get; set; }

        public float ERFSize { get; set; }

        public string About { get; set; } = string.Empty;

        public string Agent {  get; set; } = string.Empty;
    }
}
