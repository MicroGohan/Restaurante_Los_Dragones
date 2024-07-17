using Bar_Restaurante_Los_Dragones.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Dal.Dragones;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class ReporteController : Controller
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //DIA
        [HttpGet]
        public IActionResult TotalFacturadoPorDia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TotalFacturadoPorDia(DateTime? fecha)
        {
            if (!fecha.HasValue)
            {
                fecha = DateTime.Now;
            }

            if (fecha.Value.Date > DateTime.Now.Date)
            {
                ModelState.AddModelError(string.Empty, "La fecha no puede ser futura.");
                ViewBag.Fecha = fecha.Value;
                return View();
            }

            var total = await _reporteService.ObtenerTotalFacturadoPorDia(fecha.Value);
            var facturas = await _reporteService.ObtenerFacturasPorDia(fecha.Value);

            ViewBag.TotalFacturado = total;
            ViewBag.Facturas = facturas;
            ViewBag.Fecha = fecha.Value;

            return View();
        }


        //MES
        [HttpGet]
        public IActionResult TotalFacturadoPorMes()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TotalFacturadoPorMes(int mes, int año)
        {
            var currentDate = DateTime.Now;
            if (año > currentDate.Year || (año == currentDate.Year && mes > currentDate.Month))
            {
                ModelState.AddModelError(string.Empty, "No puedes seleccionar un mes incompleto.");
                return View();
            }

            var totalFacturado = _reporteService.ObtenerTotalFacturadoPorMes(mes, año);
            var facturas = _reporteService.ObtenerFacturasPorMes(mes, año);

            ViewBag.TotalFacturado = totalFacturado;
            ViewBag.Facturas = facturas;
            ViewBag.Mes = mes;
            ViewBag.Año = año;
            return View();
        }

        //FECHA PER
        [HttpGet]
        public IActionResult TotalFacturadoPorFechaSeleccionada()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TotalFacturadoPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                ModelState.AddModelError(string.Empty, "La fecha de inicio no puede ser posterior a la fecha de fin.");
                return View();
            }

            var total = await _reporteService.ObtenerTotalFacturadoPorFechaSeleccionada(fechaInicio, fechaFin);
            var facturas = await _reporteService.ObtenerFacturasPorFechaSeleccionada(fechaInicio, fechaFin);

            ViewBag.TotalFacturado = total;
            ViewBag.Facturas = facturas;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;

            return View();
        }

        //DOWNLOAD PDF POR DIA
        public async Task<IActionResult> DownloadPDF(DateTime fecha)
        {
            Console.WriteLine($"Fecha recibida: {fecha}");
            var totalFacturado = await _reporteService.ObtenerTotalFacturadoPorDia(fecha);
            var facturas = await _reporteService.ObtenerFacturasPorDia(fecha);
            var pdfBytes = GenerarPdfFacturas(facturas, $"Facturas del día {fecha.ToShortDateString()}", totalFacturado);
            return File(pdfBytes, "application/pdf", $"Facturas_{fecha.ToString("yyyyMMdd")}.pdf");
        }
        private byte[] GenerarPdfFacturas(IEnumerable<Factura> facturas, string titulo, decimal totalFacturado)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear el documento PDF
                var document = new Document(iTextSharp.text.PageSize.A4);
                PdfWriter writer = null;

                try
                {
                    // Inicializar el PdfWriter con el MemoryStream
                    writer = PdfWriter.GetInstance(document, memoryStream);

                    // Abrir el documento
                    document.Open();

                    // Agregar título
                    document.Add(new iTextSharp.text.Paragraph(titulo));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar total facturado
                    document.Add(new iTextSharp.text.Paragraph($"Total Facturado: {totalFacturado:C}"));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar las facturas al documento
                    foreach (var factura in facturas)
                    {
                        //document.Add(new iTextSharp.text.Paragraph($"Fecha: {factura.Fecha.ToString("dd/MM/yyyy")}"));
                        document.Add(new iTextSharp.text.Paragraph($"Nombre Cliente: {factura.NombreCliente}"));
                        document.Add(new iTextSharp.text.Paragraph($"Subtotal: {factura.Subtotal}"));
                        document.Add(new iTextSharp.text.Paragraph($"Descuento: {factura.Descuento}"));
                        document.Add(new iTextSharp.text.Paragraph($"IVA: {factura.Iva}"));
                        document.Add(new iTextSharp.text.Paragraph($"Total a Pagar: {factura.TotalPagar}"));
                        document.Add(new iTextSharp.text.Paragraph(""));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según tu necesidad
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
                finally
                {
                    // Cerrar el documento y liberar recursos
                    if (document != null)
                    {
                        document.Close();
                    }
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }

                // Devolver los bytes del MemoryStream
                return memoryStream.ToArray();
            }
        }

        //DOWNLOAD PDF POR MES

        public async Task<IActionResult> DownloadPDFPorMes(int mes, int año)
        {
            Console.WriteLine($"Mes y Año recibidos: {mes}, {año}");

            // Obtener el total facturado por mes de manera asincrónica
            var totalFacturado = await _reporteService.ObtenerTotalFacturadoPorMes(mes, año);

            // Obtener las facturas por mes de manera asincrónica
            var facturas = _reporteService.ObtenerFacturasPorMes(mes, año);

            // Generar el PDF con los datos obtenidos
            var pdfBytes = GenerarPdfFacturasPorMes(facturas, $"Facturas del mes {mes}/{año}", totalFacturado);

            // Devolver el archivo PDF como resultado de la acción
            return File(pdfBytes, "application/pdf", $"Facturas_{año}_{mes.ToString("D2")}.pdf");
            
        }

        private byte[] GenerarPdfFacturasPorMes(IEnumerable<Factura> facturas, string titulo, decimal totalFacturado)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear el documento PDF
                var document = new Document(iTextSharp.text.PageSize.A4);
                PdfWriter writer = null;

                try
                {
                    // Inicializar el PdfWriter con el MemoryStream
                    writer = PdfWriter.GetInstance(document, memoryStream);

                    // Abrir el documento
                    document.Open();

                    // Agregar título
                    document.Add(new iTextSharp.text.Paragraph(titulo));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar total facturado
                    document.Add(new iTextSharp.text.Paragraph($"Total Facturado: {totalFacturado:C}"));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar las facturas al documento
                    foreach (var factura in facturas)
                    {
                        document.Add(new iTextSharp.text.Paragraph($"Fecha: {factura.Fecha.ToString("dd/MM/yyyy")}"));
                        document.Add(new iTextSharp.text.Paragraph($"Nombre Cliente: {factura.NombreCliente}"));
                        document.Add(new iTextSharp.text.Paragraph($"Subtotal: {factura.Subtotal:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"Descuento: {factura.Descuento:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"IVA: {factura.Iva:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"Total a Pagar: {factura.TotalPagar:C}"));
                        document.Add(new iTextSharp.text.Paragraph(""));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según tu necesidad
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
                finally
                {
                    // Cerrar el documento y liberar recursos
                    if (document != null)
                    {
                        document.Close();
                    }
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }

                // Devolver los bytes del MemoryStream
                return memoryStream.ToArray();
            }
        }

        //DOWNLOAD PDF POR FECHA SELECT
        public async Task<IActionResult> DownloadPDFPorFechaSeleccionada(DateTime fechaInicio, DateTime fechaFin)
        {
            var totalFacturado = await _reporteService.ObtenerTotalFacturadoPorFechaSeleccionada(fechaInicio, fechaFin);
            var facturas = await _reporteService.ObtenerFacturasPorFechaSeleccionada(fechaInicio, fechaFin);
            var pdfBytes = GenerarPdfFacturasPorFecha(facturas, $"Facturas del {fechaInicio.ToShortDateString()} al {fechaFin.ToShortDateString()}", totalFacturado);

            return File(pdfBytes, "application/pdf", $"Facturas_{fechaInicio.ToString("yyyyMMdd")}_{fechaFin.ToString("yyyyMMdd")}.pdf");
        }
        private byte[] GenerarPdfFacturasPorFecha(IEnumerable<Factura> facturas, string titulo, decimal totalFacturado)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear el documento PDF
                var document = new Document(iTextSharp.text.PageSize.A4);
                PdfWriter writer = null;

                try
                {
                    // Inicializar el PdfWriter con el MemoryStream
                    writer = PdfWriter.GetInstance(document, memoryStream);

                    // Abrir el documento
                    document.Open();

                    // Agregar título
                    document.Add(new iTextSharp.text.Paragraph(titulo));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar total facturado
                    document.Add(new iTextSharp.text.Paragraph($"Total Facturado: {totalFacturado:C}"));
                    document.Add(new iTextSharp.text.Paragraph(""));

                    // Agregar las facturas al documento
                    foreach (var factura in facturas)
                    {
                        document.Add(new iTextSharp.text.Paragraph($"Fecha: {factura.Fecha.ToString("dd/MM/yyyy")}"));
                        document.Add(new iTextSharp.text.Paragraph($"Nombre Cliente: {factura.NombreCliente}"));
                        document.Add(new iTextSharp.text.Paragraph($"Subtotal: {factura.Subtotal:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"Descuento: {factura.Descuento:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"IVA: {factura.Iva:C}"));
                        document.Add(new iTextSharp.text.Paragraph($"Total a Pagar: {factura.TotalPagar:C}"));
                        document.Add(new iTextSharp.text.Paragraph(""));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según tu necesidad
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
                finally
                {
                    // Cerrar el documento y liberar recursos
                    if (document != null)
                    {
                        document.Close();
                    }
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }

                // Devolver los bytes del MemoryStream
                return memoryStream.ToArray();
            }
        }
    }
}
