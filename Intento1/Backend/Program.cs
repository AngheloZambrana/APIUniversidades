using AutoMapper;
using Backend.Mappers;
using Backend.Services;
using Backend.Services.Interfaces;
using DB;
using DB.SingleDAO;
using Microsoft.OpenApi.Models;

// Abrir conexión a la base de datos
DBConnector.OpenConnection();

// Limpiar tablas e inyectar datos iniciales
DBInjector.TruncateAllTables();
DBInjector.InjectData();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Work", Version = "v1" });
    c.AddServer(new OpenApiServer { Url = "/" }); // Configuración para que Swagger esté accesible en la raíz
});

// Agregar servicios básicos
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Registrar manualmente los DAOs y servicios relacionados
builder.Services.AddScoped<IUniversidadesDAO, UniversidadesDAO>();
builder.Services.AddScoped<IUniversidadesServices, UniversidadesService>();  
builder.Services.AddScoped<IBecaDAO, BecaDAO>();
builder.Services.AddScoped<IBecaServices, BecaService>();  
builder.Services.AddScoped<IFacultadDAO, FacultadesDAO>();
builder.Services.AddScoped<IFacultadesServices, FacultadesService>(); 
builder.Services.AddScoped<ICarreraDAO, CarrerasDAO>();
builder.Services.AddScoped<ICarrerasServices, CarrerasService>();

// Configuración de CORS
builder.Services.AddCors(policyBuilder =>
{
    policyBuilder.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

// Configurar Swagger en modo de desarrollo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Work v1"); // Cambié la URL para que apunte correctamente
    c.RoutePrefix = string.Empty; // Hacer Swagger accesible en la raíz (sin necesidad de /Universidades)
});

// Middleware y configuración de rutas
app.MapGet("/", () => "Hello World!");
app.UseCors("AllowLocalhost");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
