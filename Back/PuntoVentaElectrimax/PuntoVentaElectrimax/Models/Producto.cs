using System;
using System.Collections.Generic;

namespace PuntoVentaElectrimax.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Conceptos = new HashSet<Concepto>();
        }

        public int Id { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal Precio { get; set; }
        public float Descuento { get; set; }

        public virtual ICollection<Concepto> Conceptos { get; set; }
    }
}
