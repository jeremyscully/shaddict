using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Shaddict
{
    public class ConfigTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting configuration test...");
            
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Output results
            Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
            Console.WriteLine($"Connection string: {connectionString}");
            
            // List all configuration values
            Console.WriteLine("\nAll configuration values:");
            foreach (var pair in configuration.AsEnumerable())
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            
            Console.WriteLine("\nTest completed.");
        }
    }
} 