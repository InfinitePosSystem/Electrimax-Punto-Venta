using System.ComponentModel.DataAnnotations;

namespace PuntoVentaElectrimax.Models.Request
{
    public class VentasRequest
    {
        [Required]
        [Range(1,double.MaxValue,ErrorMessage = "El valor idCliente debe ser mayor a 0")]
        [ExisteCliente(ErrorMessage = "El cliente no existe")]
        public int IdCliente { get; set; }
        [Required]
        [MinLength(1,ErrorMessage ="Deben existir conceptos")]
        public List<Concepto> Conceptos { get; set; }
        public VentasRequest()
        {
            this.Conceptos = new List<Concepto>();

        }
    }
    public class Concepto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdProducto { get; set; }

    }
    #region validacion
    public class ExisteClienteAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int idCliente = (int)value;
            using (var db = new Models.localdb_electrimaxContext())
            {
                if(db.Clientes.Find(idCliente) == null) return false;
            }
            return true;
        }
    }

    #endregion

}
