using System;
namespace clonemondo.Models
{
	public class Flight
	{
        public string FlightNumber { get; set; }
        public Airport OriginAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OriginDepartureTime { get; set; }
        public DateTime OriginArrivalTime { get; set; }
        public DateTime DestinationDepartureTime { get; set; }
        public DateTime DestinationArrivalTime { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
    }
}
