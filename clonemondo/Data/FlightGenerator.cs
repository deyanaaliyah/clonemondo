using System;
using System.Collections.Generic;
using System.Linq;
using clonemondo.Models;

namespace clonemondo.Data
{
    public class FlightGenerator
    {
        private readonly Random random = new Random();
        private readonly AirportDataReader airportDataReader = new AirportDataReader();

        // Return a list of fake flights upon a search
        public List<Flight> GenerateFakeFlights(string originAirport, string destinationAirport, DateTime departureDate, DateTime returnDate)
        {
            // Initialise flight list and Airports
            var flights = new List<Flight>();
            Airport OriginAirport = GetAirportByName(originAirport);
            Airport DestinationAirport = GetAirportByName(destinationAirport);

            // If there's no returndate, then it's a one-way
            if (returnDate == new DateTime()) // https://stackoverflow.com/a/23784218
            {
                for (int i = 0; i < random.Next(1, 14); i++)
                {
                    var flight = new Flight
                    {
                        FlightNumber = CreateFakeFlightNumber(OriginAirport.Country),
                        OriginAirport       = OriginAirport,
                        DestinationAirport  = DestinationAirport,
                        StartDate           = departureDate,
                        Price               = GenerateRandomPrice(originAirport, destinationAirport),
                        Duration            = GenerateRandomDuration(OriginAirport, DestinationAirport),
                        DestinationDepartureTime = GenerateDepartureTime(),
                        OriginDepartureTime = GenerateDepartureTime()
                        // Arrivals are calculated in view (DepartureTime + Duration)
                    };

                    flights.Add(flight);
                }
            }
            else
            {
                for (int i = 0; i < random.Next(1, 18); i++)
                {
                    var flight = new Flight
                    {
                        FlightNumber = CreateFakeFlightNumber(OriginAirport.Country),
                        OriginAirport       = OriginAirport,
                        DestinationAirport  = DestinationAirport,
                        StartDate           = departureDate,
                        EndDate             = returnDate,
                        Price               = GenerateRandomPrice(originAirport, destinationAirport),
                        Duration            = GenerateRandomDuration(OriginAirport, DestinationAirport),
                        DestinationDepartureTime = GenerateDepartureTime(),
                        OriginDepartureTime = GenerateDepartureTime()
                        // Arrivals are calculated in view (DepartureTime + Duration)
                    };

                    flights.Add(flight);
                }
            }

            return flights;
        }

        // Create Flight number based on the Country and a set of random 4-digit numbers
        public string CreateFakeFlightNumber(string CountryOrigin)
        {
            return CountryOrigin.Substring(0, 2) + random.Next(1000, 9999);
        }

        // Get Airport object with use of LINq
        public Airport? GetAirportByName(string AirportToBeReturned)
        {
            var airports = airportDataReader.ReadAirports();

            return airports.Find(airport => airport.FullName == AirportToBeReturned);
        }

        // Return a boolean if the two airport object has the same continent value
        private bool AreAirportsInSameContinent(string originAirport, string destinationAirport)
        {
            Airport OriginAirport = GetAirportByName(originAirport);
            Airport DestinationAirport = GetAirportByName(destinationAirport);

            if (OriginAirport.Continent.Equals(DestinationAirport.Continent))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Generate a random price based on the continent where the Airports reside
        private int GenerateRandomPrice(string originAirport, string destinationAirport)
        {
            // Calculate the price based on whether the airports are in the same continent
            var isSameContinent = AreAirportsInSameContinent(originAirport, destinationAirport);

            var minPrice = isSameContinent ? 400 : 1000;
            var maxPrice = isSameContinent ? 900 : 2500;

            return random.Next(minPrice, maxPrice + 1); // Include max price
        }

        // Generate a random time duration of the flight based on its continent
        private string GenerateRandomDuration(Airport originAirport, Airport destinationAirport)
        {
            int hours;

            // If airports are in the same continent, flight duration is 1-3 hours
            if (originAirport.Continent.Equals(destinationAirport.Continent))
            {
                hours = random.Next(1, 4);
            }
            else
            {
                hours = random.Next(5, 10);
            }

            var minutes = random.Next(0, 60);
            var roundedMinutes = (int)Math.Ceiling(minutes / 5.0) * 5; // Round up to nearest 5 minutes
            if (roundedMinutes >= 60)
            {
                hours++;
                roundedMinutes = 0;
            }

            return $"{hours}h {roundedMinutes}m";
        }

        public DateTime GenerateDepartureTime()
        {
            DateTime now = DateTime.Now;
            int minutes = now.Minute;
            int roundedMinutes = (int)Math.Ceiling((double)minutes / 5) * 5; // Round up to nearest 5
            DateTime randomTimestamp = new DateTime(now.Year, now.Month, now.Day, now.Hour, roundedMinutes, 0);

            return randomTimestamp;
        }
    }
}
