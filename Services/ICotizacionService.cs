using CotizacionService.Models;

namespace CotizacionService.Services;

public interface ICotizacionService
{
    CotizacionResponse? Cotizar(CotizacionRequest request);
}
