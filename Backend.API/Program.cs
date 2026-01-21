using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
using PharmacyEmergencySystem.Services; 
=======
using PharmacyEmergencySystem.Services; // ✅ Add this
>>>>>>> 1b6a11dd1693c42b7ca7e57865e30d9a3d94c429

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();

// Register other services
builder.Services.AddSingleton<MatchingService>();
builder.Services.AddSingleton<DeliverySimulationService>();
builder.Services.AddSingleton<ExternalApiService>();
<<<<<<< HEAD
builder.Services.AddScoped<OtpService>();


var app = builder.Build();

// This is where you put the Swagger code
=======

var app = builder.Build();

// ✅ This is where you put the Swagger code
>>>>>>> 1b6a11dd1693c42b7ca7e57865e30d9a3d94c429
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the app
app.Run();

