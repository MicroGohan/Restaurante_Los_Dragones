using Dal.Dragones;

namespace Bar_Restaurante_Los_Dragones.Services
{
    public interface IReporteService
    {   
        //Dia
        Task<decimal> ObtenerTotalFacturadoPorDia(DateTime fecha);
        Task<List<Factura>> ObtenerFacturasPorDia(DateTime fecha);

        //Mes
        Task<decimal> ObtenerTotalFacturadoPorMes(int mes, int año);
        List<Factura> ObtenerFacturasPorMes(int mes, int año);

        //Fecha Per
        Task<decimal> ObtenerTotalFacturadoPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin);
        Task<List<Factura>> ObtenerFacturasPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin);
    }
}