using BibliotecaAPI;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// área de servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddSingleton<IRepositorioValores, RepositorioValoresOracle>();

builder.Services.AddControllers().AddJsonOptions(opciones =>
opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// área de middlewares


app.Use(async (contexto, next) =>
{
    // Viene la petición
    var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Petición: {contexto.Request.Method} {contexto.Request.Path}");

    await next.Invoke();
    
    // Se va la respuesta 

    logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
});

app.Use(async (contexto, next) =>
{
    if (contexto.Request.Path == "/bloqueado")
    {
        contexto.Response.StatusCode = 403;
        await contexto.Response.WriteAsync("Acceso denegado");
    }
    else
    {
        await next.Invoke();
    }
});




app.MapControllers();

app.Run();