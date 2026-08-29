namespace CotizacionService.Models;

public class CotizacionResponse
{
    public string Producto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public TipoCliente TipoCliente { get; set; }
    public decimal PrecioBaseUnitario { get; set; }
    public decimal PorcentajeDescuento { get; set; }
    public decimal PrecioUnitarioConDescuento { get; set; }
    public decimal Total { get; set; }
}
