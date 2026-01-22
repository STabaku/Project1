using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PharmacyEmergencySystem.Services;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// SERVICES
// ===============================

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (shumë e rëndësishme për frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Regjistro service-t e aplikacionit
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OtpService>();

builder.Services.AddSingleton<MatchingService>();
builder.Services.AddSingleton<DeliverySimulationService>();
builder.Services.AddSingleton<ExternalApiService>();

var app = builder.Build();

// ===============================
// MIDDLEWARE
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aktivizo CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run app
app.Run();
