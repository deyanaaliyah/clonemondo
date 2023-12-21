using System;
using clonemondo.Models;
using System.Collections.Generic;
using System.IO;

namespace clonemondo.Data
{
    public class AirportDataReader
    {
        // Encapsulate the path of the CSV file
        private string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FilePath = Path.Combine(currentDirectory, "Data", "airports.csv");

            return FilePath;
        }

        public List<Airport> ReadAirports()
        {
            string FilePath = GetFilePath();

            List<Airport> airports = new();

            // If the path is correct and the file exists
            if (File.Exists(FilePath))
            {
                // Seperate the file by lines and put them into a string
                string[] lines = File.ReadAllLines(FilePath);

                // Loop through the lines
                foreach (string line in lines)
                {
                    // And now, split each line by ";" and range+add them onto a new airport
                    string[] values = line.Split(';');

                    if (values.Length >= 6)
                    {
                        airports.Add(new Airport
                        {
                            Abbreviation = values[0],
                            City = values[1],
                            Country = values[2],
                            Continent = values[3],
                            Airline = values[4],
                            FullName = values[1] + " (" + values[0] + ")" // I love this. This is nice.
                        });
                    }
                }
            }

            return airports;
        }

        // The "?" allows the method to be null
        public Airport? GetAirport(string MySearch)
        {
            List<Airport> airports = ReadAirports();

            return airports.FirstOrDefault(x => x.FullName == MySearch);
        }
    }
}
