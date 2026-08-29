namespace CotizacionService.Strategies;

// Cada implementación encapsula UNA forma de calcular el descuento.
// Agregar un nuevo tipo de cliente = agregar una nueva clase, sin tocar
// las que ya existen (principio Abierto/Cerrado).
public interface IDescuentoStrategy
{
    // Devuelve el porcentaje de descuento (0.0 a 1.0) para una cantidad dada.
    decimal CalcularPorcentajeDescuento(int cantidad);
}
