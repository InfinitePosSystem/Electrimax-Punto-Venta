using System;
using System.Collections.Generic;

namespace PuntoVentaElectrimax.Models
{
    public partial class Concepto
    {
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
