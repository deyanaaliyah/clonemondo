using System;
using clonemondo.Models;

namespace clonemondo.ViewModels
{
    public class SearchResultViewModel
    {
        public List<Flight> Flights { get; set; }
        public string InitialFlightCarrierURL { get; set; }
        public string ReturnFlightCarrierURL { get; set; }
    }
}
