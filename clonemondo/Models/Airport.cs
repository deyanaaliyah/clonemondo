using System;
namespace clonemondo.Models
{
	public class Airport
	{
        public string Abbreviation { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Airline { get; set; }
        public string FullName { get; set; }
    }

    public class AirportViewModel
    {
        public List<Airport> Airports { get; set; }
    }
}
