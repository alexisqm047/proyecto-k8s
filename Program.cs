using System.Text.Json.Serialization;
using CotizacionService.Models;
using CotizacionService.Repositories;
using CotizacionService.Services;
using CotizacionService.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Permite enviar/recibir el enum TipoCliente como texto ("Regular", "Vip", "Mayorista")
// en vez de números, tanto en el JSON de entrada como en el de salida.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// --- Inyección de dependencias ---
// Registramos por interfaz, no por implementación: el resto del código
// nunca depende de detalles concretos (Dependency Inversion Principle).
builder.Services.AddSingleton<IProductoRepository, ProductoRepository>();
builder.Services.AddSingleton<DescuentoStrategyFactory>();
builder.Services.AddScoped<ICotizacionService, CotizacionServiceImpl>();

// Health checks nativos de ASP.NET Core (sin paquetes NuGet externos).
// Kubernetes los usará como liveness/readiness probe.
builder.Services.AddHealthChecks();

var app = builder.Build();

// Sirve el index.html + assets de wwwroot (la UI estática).
app.UseDefaultFiles();
app.UseStaticFiles();

// --- Endpoints ---

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
   .WithName("Health");

app.MapGet("/api/productos", (IProductoRepository repo) =>
    Results.Ok(repo.ObtenerTodos()))
   .WithName("ObtenerProductos");

app.MapPost("/api/cotizacion", (CotizacionRequest request, ICotizacionService cotizacionService) =>
{
    if (request.Cantidad <= 0)
    {
        return Results.BadRequest(new { error = "La cantidad debe ser mayor a cero." });
    }

    var resultado = cotizacionService.Cotizar(request);

    return resultado is null
        ? Results.NotFound(new { error = $"El producto con id {request.ProductoId} no existe." })
        : Results.Ok(resultado);
})
.WithName("CrearCotizacion");

app.Run();
