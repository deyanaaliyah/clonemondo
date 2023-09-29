using System;
using System.Collections.Generic;
using System.IO;
using clonemondo.Models;

namespace clonemondo.Data
{
    public class FeaturedFaresGenerator
    {
        private readonly Random random = new Random();

        private string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Data", "FeaturedFares.csv");
            return filePath;
        }

        private List<FeaturedFares> ReadFeaturedFares()
        {
            string filePath = GetFilePath();
            List<FeaturedFares> featuredFares = new List<FeaturedFares>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(';');

                    if (values.Length >= 4)
                    {
                        featuredFares.Add(new FeaturedFares
                        {
                            ImageUrl = values[0],
                            City = values[1],
                            Country = values[2],
                            Continent = values[3]
                        });
                    }
                }
            }

            return featuredFares;
        }

        public int SetPrice(string continent)
        {
            int randomPrice;

            if (continent.Equals("Europe"))
            {
                randomPrice = random.Next(251, 499);
            }
            else
            {
                randomPrice = random.Next(901, 1599);
            }

            return randomPrice;
        }

        public List<FeaturedFares> GenerateFeaturedFares()
        {
            List<FeaturedFares> featuredFares = new List<FeaturedFares>();
            List<FeaturedFares> allFares = ReadFeaturedFares();

            // Ensure there are enough fares in the CSV file
            if (allFares.Count < 6)
            {
                throw new InvalidOperationException("Not enough fares in the CSV file.");
            }

            // Generate 6 random fares
            for (int i = 0; i < 6; i++)
            {
                int randomIndex = random.Next(allFares.Count);
                var fare = allFares[randomIndex];

                // Generate random duration text
                DateTime randomDate = DateTime.Now.AddDays(random.Next(1, 30));
                string duration = $"Book a returning flight from {randomDate:dd MMM}";

                featuredFares.Add(new FeaturedFares
                {
                    ImageUrl = fare.ImageUrl,
                    City = fare.City,
                    Country = fare.Country,
                    Duration = duration,
                    PriceRange = SetPrice(fare.Continent)
                });

                // Remove the used fare to avoid duplicates
                allFares.RemoveAt(randomIndex);
            }

            return featuredFares;
        }
    }
}
