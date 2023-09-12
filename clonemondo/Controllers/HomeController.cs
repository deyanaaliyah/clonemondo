using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using clonemondo.Models;
using clonemondo.Data;
using clonemondo.ViewModels;

namespace clonemondo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Month = DateTime.Now.ToString("MMMM");

        var featuredFaresGenerator = new FeaturedFaresGenerator();
        var featuredFares = featuredFaresGenerator.GenerateFeaturedFares();
        var airportDataReader = new AirportDataReader();
        var airports = airportDataReader.ReadAirports();

        var viewModel = new IndexViewModel
        {
            FeaturedFares = featuredFares,
            Airports = airports
        };

        return View(viewModel);
    }

    public IActionResult Airports()
    {
        var airportDataReader = new AirportDataReader();
        var airports = airportDataReader.ReadAirports();

        // Log the count of airports for debugging
        Console.WriteLine($"Number of airports: {airports.Count}");

        var model = new AirportViewModel
        {
            Airports = airports
        };

        return View(model);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

