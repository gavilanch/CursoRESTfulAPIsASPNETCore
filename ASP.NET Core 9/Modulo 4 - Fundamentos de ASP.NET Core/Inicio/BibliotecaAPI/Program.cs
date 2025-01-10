using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// área de servicios

builder.Services.AddControllers().AddJsonOptions(opciones =>
opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// área de middlewares

app.MapControllers();

app.Run();
