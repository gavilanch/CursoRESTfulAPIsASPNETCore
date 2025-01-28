using BibliotecaAPI;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var diccionarioConfiguraciones = new Dictionary<string, string>
{
    {"quien_soy", "un diccionario en memoria" }
};

builder.Configuration.AddInMemoryCollection(diccionarioConfiguraciones!);

// área de servicios

builder.Services.AddOptions<PersonaOpciones>()
    .Bind(builder.Configuration.GetSection(PersonaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<TarifaOpciones>()
    .Bind(builder.Configuration.GetSection(TarifaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<PagosProcesamiento>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// área de middlewares

app.MapControllers();

app.Run();