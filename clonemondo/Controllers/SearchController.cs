using System;
using System.Collections.Generic;
using System.Linq;
using clonemondo.Data;
using clonemondo.Models;
using clonemondo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace clonemondo.Controllers
{
    public class SearchController : Controller
    {
        private readonly AirportDataReader _airportDataReader;
        private readonly FlightGenerator _flightGenerator;

        public SearchController()
        {
            _airportDataReader = new AirportDataReader();
            _flightGenerator = new FlightGenerator();
        }

        public IActionResult SearchFlights(string departure, string destination, DateTime departureDate, DateTime returnDate)
        {
            // Generate dummy flights based on user input
            var dummyFlights = _flightGenerator.GenerateFakeFlights(
                departure,
                destination,
                departureDate,
                returnDate);

            // Create an instance of SearchResultViewModel and populate the 'Flights' property
            var viewModel = new SearchResultViewModel
            {
                Flights = dummyFlights,
                InitialFlightCarrierURL = "" + departure    + ".png",
                ReturnFlightCarrierURL  = "" + destination  + ".png"
            };

            return View("SearchResult", viewModel);
        }

    }
}
