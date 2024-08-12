public class CalculadoraDeCuentas
{
    /// <summary>
    /// Divide el total de la compra entre el número de comensales.
    /// </summary>
    /// <param name="totalCompra">El total de la compra.</param>
    /// <param name="numeroComensales">El número de comensales.</param>
    /// <returns>El monto que cada persona debe pagar.</returns>
    /// <exception cref="ArgumentException">Si el número de comensales es menor o igual a cero.</exception>
    public decimal DividirCuenta(decimal totalCompra, int numeroComensales)
    {
        if (numeroComensales <= 0)
        {
            throw new ArgumentException("El número de comensales debe ser mayor que cero.");
        }

        return totalCompra / numeroComensales;
    }
}
