using CotizacionService.Models;
using CotizacionService.Repositories;
using CotizacionService.Strategies;

namespace CotizacionService.Services;

public class CotizacionServiceImpl : ICotizacionService
{
    private readonly IProductoRepository _productoRepository;
    private readonly DescuentoStrategyFactory _descuentoFactory;
    private readonly ILogger<CotizacionServiceImpl> _logger;

    public CotizacionServiceImpl(
        IProductoRepository productoRepository,
        DescuentoStrategyFactory descuentoFactory,
        ILogger<CotizacionServiceImpl> logger)
    {
        _productoRepository = productoRepository;
        _descuentoFactory = descuentoFactory;
        _logger = logger;
    }

    public CotizacionResponse? Cotizar(CotizacionRequest request)
    {
        var producto = _productoRepository.ObtenerPorId(request.ProductoId);
        if (producto is null)
        {
            _logger.LogWarning("Producto {ProductoId} no encontrado", request.ProductoId);
            return null;
        }

        var estrategia = _descuentoFactory.Obtener(request.TipoCliente);
        var porcentajeDescuento = estrategia.CalcularPorcentajeDescuento(request.Cantidad);
        var precioUnitarioConDescuento = producto.PrecioBase * (1 - porcentajeDescuento);
        var total = precioUnitarioConDescuento * request.Cantidad;

        _logger.LogInformation(
            "Cotización generada: producto={Producto}, cantidad={Cantidad}, tipoCliente={TipoCliente}, descuento={Descuento}%",
            producto.Nombre, request.Cantidad, request.TipoCliente, porcentajeDescuento * 100);

        return new CotizacionResponse
        {
            Producto = producto.Nombre,
            Cantidad = request.Cantidad,
            TipoCliente = request.TipoCliente,
            PrecioBaseUnitario = producto.PrecioBase,
            PorcentajeDescuento = porcentajeDescuento,
            PrecioUnitarioConDescuento = precioUnitarioConDescuento,
            Total = total
        };
    }
}
