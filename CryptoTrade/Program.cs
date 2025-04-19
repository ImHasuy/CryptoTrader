using CryptoTrade.Context;
using CryptoTrade.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crypto Trade API", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please insert JWT token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//    {
//        new OpenApiSecurityScheme {
//            Reference = new OpenApiReference {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//            }
//        },
//        new string[] { }
//    }});
//});




builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
