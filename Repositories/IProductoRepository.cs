using CotizacionService.Models;

namespace CotizacionService.Repositories;

public interface IProductoRepository
{
    IEnumerable<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
}
