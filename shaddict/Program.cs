using Microsoft.EntityFrameworkCore;
using Shaddict.Data;
using System;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the database context with explicit namespace
Console.WriteLine("Registering ShadDictContext...");

// Use a hardcoded connection string
var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Shaddict;Trusted_Connection=True;MultipleActiveResultSets=true";
Console.WriteLine($"Using hardcoded connection string: {connectionString}");

builder.Services.AddDbContext<Shaddict.Data.ShadDictContext>(options => 
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
    options.LogTo(Console.WriteLine);
});

Console.WriteLine("ShadDictContext registered successfully.");

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Console.WriteLine("Attempting to resolve ShadDictContext...");
        var context = services.GetRequiredService<Shaddict.Data.ShadDictContext>();
        Console.WriteLine("ShadDictContext resolved successfully.");
        
        // Ensure database is created
        Console.WriteLine("Ensuring database is created...");
        context.Database.EnsureCreated();
        Console.WriteLine("Database created successfully.");
        
        // Initialize with sample data if needed
        Console.WriteLine("Initializing database with sample data...");
        Shaddict.Data.DbInitializer.Initialize(context);
        Console.WriteLine("Database initialized successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing database: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
