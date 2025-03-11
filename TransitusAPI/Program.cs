using Microsoft.OpenApi.Models;
using System.Reflection;
using TransitusAPI.Data;
using Microsoft.AspNetCore.Server.HttpSys;

var builder = WebApplication.CreateBuilder(args);

// Configure HTTP.sys
builder.WebHost.UseHttpSys(options =>
{
    options.UrlPrefixes.Add("http://localhost:5000");
});

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register database connection and repositories
builder.Services.AddSingleton<DatabaseConnection>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transitus API",
        Version = "v1",
        Description = "API for the Transitus application",
        Contact = new OpenApiContact
        {
            Name = "Transitus Support",
            Email = "support@transitus.com"
        }
    });
    
    // Enable XML comments in Swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Transitus API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    
    // Use CORS in development
    app.UseCors("AllowAll");
}
else
{
    // In production, you might want to use a more restrictive CORS policy
    app.UseCors("AllowAll"); // For now, using the same policy in production
}

app.UseAuthorization();

app.MapControllers();

app.Run();
