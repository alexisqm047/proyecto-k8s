namespace CotizacionService.Strategies;

// Cliente VIP: descuento base más alto, con un extra si compra en volumen.
public class DescuentoVipStrategy : IDescuentoStrategy
{
    public decimal CalcularPorcentajeDescuento(int cantidad)
    {
        decimal descuento = 0.10m; // 10% base para todos los VIP
        if (cantidad >= 5) descuento += 0.05m; // +5% si compra 5 o más
        return descuento;
    }
}
