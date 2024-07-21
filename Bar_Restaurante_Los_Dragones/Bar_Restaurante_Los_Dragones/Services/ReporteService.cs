using Microsoft.EntityFrameworkCore;
using Bar_Restaurante_Los_Dragones.Models;
using Dal.Dragones;  // Asegúrate de usar el namespace correcto para tu DbContext

namespace Bar_Restaurante_Los_Dragones.Services
{
    public class ReporteService : IReporteService
    {
        private readonly ProyectoContext _context;

        public ReporteService(ProyectoContext context)
        {
            _context = context;
        }

        //DIA
        public async Task<decimal> ObtenerTotalFacturadoPorDia(DateTime fecha)
        {
            return await _context.Facturas
                .Where(f => f.Fecha.Date == fecha.Date)
                .SumAsync(f => f.TotalPagar);
        }
        public async Task<List<Factura>> ObtenerFacturasPorDia(DateTime fecha)
        {
            return await _context.Facturas
                .Where(f => f.Fecha.Date == fecha.Date)
                .ToListAsync();
        }

        //MES
        public Task<decimal> ObtenerTotalFacturadoPorMes(int mes, int año)
        {
            var totalFacturado = _context.Facturas
                .Where(f => f.Fecha.Month == mes && f.Fecha.Year == año)
                .Sum(f => (decimal?)f.TotalPagar) ?? 0;

            return Task.FromResult(totalFacturado);
        }
        public List<Factura> ObtenerFacturasPorMes(int mes, int año)
        {
            var facturas = _context.Facturas
                .Where(f => f.Fecha.Month == mes && f.Fecha.Year == año)
                .ToList();

            return facturas;
        }

        //FECHA PER
        public async Task<decimal> ObtenerTotalFacturadoPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Facturas
                .Where(f => f.Fecha.Date >= fechaInicio.Date && f.Fecha.Date <= fechaFin.Date)
                .SumAsync(f => f.TotalPagar);
        }
        public async Task<List<Factura>> ObtenerFacturasPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Facturas
                .Where(f => f.Fecha.Date >= fechaInicio.Date && f.Fecha.Date <= fechaFin.Date)
                .ToListAsync();
        }
    }
}
