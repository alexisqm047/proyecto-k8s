namespace CotizacionService.Models;

public class CotizacionRequest
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; } = 1;
    public TipoCliente TipoCliente { get; set; } = TipoCliente.Regular;
}
