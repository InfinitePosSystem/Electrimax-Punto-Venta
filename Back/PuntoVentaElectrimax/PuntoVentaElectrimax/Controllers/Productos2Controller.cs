using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaElectrimax.Models;
using PuntoVentaElectrimax.Models.Response;
using PuntoVentaElectrimax.Models.Request;
using Microsoft.AspNetCore.Authorization;

namespace PuntoVentaElectrimax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Productos2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (localdb_electrimaxContext db = new localdb_electrimaxContext())
                {
                    var lst = db.Productos.OrderByDescending(d => d.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;

                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPost]
        public IActionResult Add(ProductoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (localdb_electrimaxContext db = new localdb_electrimaxContext())
                {
                    Producto oProducto = new Producto();
                    oProducto.NombreProducto = oModel.NombreProducto;
                    oProducto.Precio = oModel.Precio;
                    oProducto.Descuento = oModel.Descuento;
                    db.Productos.Add(oProducto);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;

                }
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPut]
        public IActionResult Edit(ProductoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (localdb_electrimaxContext db = new localdb_electrimaxContext())
                {
                    Producto oProducto = db.Productos.Find(oModel.Id);
                    oProducto.NombreProducto = oModel.NombreProducto;
                    oProducto.Precio = oModel.Precio;
                    oProducto.Descuento = oModel.Descuento;
                    db.Update(oProducto);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;

                }
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (localdb_electrimaxContext db = new localdb_electrimaxContext())
                {
                    Producto oCliente = db.Productos.Find(Id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;

                }
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

    }
}
