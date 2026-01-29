using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Backend.API.Services;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;
using Backend.API.Middleware;




var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB & Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OtpService>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddSingleton<DeliverySimulationService>();
builder.Services.AddSingleton<ExternalApiService>();
builder.Services.AddScoped<JwtService>();


// CORS — only one policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use the single CORS policy
app.UseCors("AllowFrontend");
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
