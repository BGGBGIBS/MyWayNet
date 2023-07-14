using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using MyWayNet.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env.dev file
Env.Load(".env.dev");

// Get connection string from environment variables
var connectionString = Env.GetString("CONNECTION_STRING");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyWayContext>(options =>
    options.UseSqlServer(connectionString)
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
