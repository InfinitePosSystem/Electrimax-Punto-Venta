using System;
using System.Collections.Generic;

namespace PuntoVentaElectrimax.Models
{
    public partial class Usuario
    {
        public uint Id { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
