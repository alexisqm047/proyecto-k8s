using CotizacionService.Models;

namespace CotizacionService.Repositories;

// Implementación en memoria. El día que se necesite persistencia real (SQL, Mongo, etc.)
// solo se crea una nueva clase que implemente IProductoRepository; nada más en el
// proyecto tiene que cambiar, porque el resto del código solo conoce la interfaz.
public class ProductoRepository : IProductoRepository
{
    private readonly List<Producto> _productos = new()
    {
        new Producto { Id = 1, Nombre = "Laptop 14\" Pro",      Categoria = "Tecnología", PrecioBase = 3200000m },
        new Producto { Id = 2, Nombre = "Mouse inalámbrico",    Categoria = "Tecnología", PrecioBase = 85000m },
        new Producto { Id = 3, Nombre = "Silla ergonómica",     Categoria = "Oficina",    PrecioBase = 650000m },
        new Producto { Id = 4, Nombre = "Monitor 27\" 4K",      Categoria = "Tecnología", PrecioBase = 1450000m },
        new Producto { Id = 5, Nombre = "Escritorio ajustable", Categoria = "Oficina",    PrecioBase = 980000m },
    };

    public IEnumerable<Producto> ObtenerTodos() => _productos;

    public Producto? ObtenerPorId(int id) => _productos.FirstOrDefault(p => p.Id == id);
}
