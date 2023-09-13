using System;
using clonemondo.Models;
using System.Collections.Generic;
using System.IO;

namespace clonemondo.Data
{
    public class AirportDataReader
    {
        private string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FilePath = Path.Combine(currentDirectory, "Data", "airports.csv");

            return FilePath;
        }

        public List<Airport> ReadAirports()
        {
            string FilePath = GetFilePath();

            List<Airport> airports = new List<Airport>();

            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
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
                            FullName = values[1] + " (" + values[0] + ")"
                        });
                    }
                }
            }

            return airports;
        }
    }
}
