using System.ComponentModel.DataAnnotations;

namespace PuntoVentaElectrimax.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
