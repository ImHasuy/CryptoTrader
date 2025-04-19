using CryptoTrade.Context;
using CryptoTrade.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnectionAtHome");
    optionsBuilder.UseSqlServer(connectionString);
});

//Scoped
builder.Services.AddScoped<IUserServicecs, UserService>();
builder.Services.AddScoped<IUnitOfWork, ProductionUnitOfWork>();


//Scoped\\



builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
