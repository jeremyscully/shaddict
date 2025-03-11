using Microsoft.Extensions.Configuration;
using System;
using System.IO;

class TestConfig
{
    static void Main()
    {
        Console.WriteLine("Testing configuration loading...");
        Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
        
        // List all files in the current directory
        Console.WriteLine("\nFiles in current directory:");
        foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
        {
            Console.WriteLine($"- {Path.GetFileName(file)}");
        }
        
        // Try to load the configuration
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                
            var configuration = builder.Build();
            
            // Check if ConnectionStrings section exists
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            if (!connectionStringsSection.Exists())
            {
                Console.WriteLine("\nERROR: ConnectionStrings section not found in appsettings.json!");
            }
            else
            {
                Console.WriteLine("\nConnectionStrings section found.");
                foreach (var child in connectionStringsSection.GetChildren())
                {
                    Console.WriteLine($"- {child.Key}: {child.Value}");
                }
            }
            
            // Try to get the DefaultConnection
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"\nDefaultConnection: {connectionString ?? "NULL"}");
            
            // Print all configuration values
            Console.WriteLine("\nAll configuration values:");
            foreach (var pair in configuration.AsEnumerable())
            {
                Console.WriteLine($"- {pair.Key}: {pair.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nERROR loading configuration: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
} 