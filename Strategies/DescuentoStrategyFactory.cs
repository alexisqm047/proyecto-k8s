using CotizacionService.Models;

namespace CotizacionService.Strategies;

public class DescuentoStrategyFactory
{
    public IDescuentoStrategy Obtener(TipoCliente tipoCliente) => tipoCliente switch
    {
        TipoCliente.Vip => new DescuentoVipStrategy(),
        TipoCliente.Mayorista => new DescuentoMayoristaStrategy(),
        _ => new DescuentoRegularStrategy()
    };
}
