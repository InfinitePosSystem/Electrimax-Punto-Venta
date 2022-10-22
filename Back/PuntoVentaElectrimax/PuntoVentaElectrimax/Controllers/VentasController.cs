using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaElectrimax.Models;
using PuntoVentaElectrimax.Models.Request;
using PuntoVentaElectrimax.Models.Response;

namespace PuntoVentaElectrimax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentasController : ControllerBase
    {
        public IActionResult Add(VentasRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (localdb_electrimaxContext db = new localdb_electrimaxContext())
                {
                    using (var transaction = db.Database.BeginTransaction()) {
                        try { 
                            var venta = new Ventum();
                            venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = model.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();
                            foreach (var concepto in model.Conceptos) {
                                var concepto1 = new Models.Concepto();
                                concepto1.Cantidad = concepto.Cantidad;
                                concepto1.IdProducto = concepto.IdProducto;
                                concepto1.PrecioUnitario = concepto.PrecioUnitario;
                                concepto1.Importe = concepto.Cantidad * concepto.PrecioUnitario;
                                concepto1.IdVenta = venta.Id;
                                db.Conceptos.Add(concepto1);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                            respuesta.Exito = 1;
                        } catch (Exception ex) {
                            transaction.Rollback();
                            respuesta.Mensaje = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                BadRequest(respuesta);
            }
            return Ok(respuesta);
        }
    }
}
