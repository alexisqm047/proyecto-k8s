namespace CotizacionService.Strategies;

// Mayorista: el descuento escala fuerte con la cantidad, porque el negocio
// depende de mover volumen alto.
public class DescuentoMayoristaStrategy : IDescuentoStrategy
{
    public decimal CalcularPorcentajeDescuento(int cantidad)
    {
        if (cantidad >= 100) return 0.25m;
        if (cantidad >= 50) return 0.18m;
        if (cantidad >= 20) return 0.12m;
        return 0.08m; // descuento mínimo garantizado por ser mayorista
    }
}
