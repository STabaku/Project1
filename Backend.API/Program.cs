using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Backend.API.Services;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;



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
builder.Services.AddSingleton<RequestService>();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://127.0.0.1:5501") // your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowFrontend");

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
