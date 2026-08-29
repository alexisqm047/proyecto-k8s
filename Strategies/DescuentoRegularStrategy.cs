namespace CotizacionService.Strategies;

// Cliente regular: sin descuento fijo, solo un pequeño incentivo por volumen.
public class DescuentoRegularStrategy : IDescuentoStrategy
{
    public decimal CalcularPorcentajeDescuento(int cantidad)
    {
        if (cantidad >= 10) return 0.03m; // 3% a partir de 10 unidades
        return 0m;
    }
}
