using System;
namespace clonemondo.Models
{
	public class Flight
	{
        public Airport OriginAirport { get; set; } // Travel from
        public Airport DestinationAirport { get; set; } // Travel to
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
    }
}
