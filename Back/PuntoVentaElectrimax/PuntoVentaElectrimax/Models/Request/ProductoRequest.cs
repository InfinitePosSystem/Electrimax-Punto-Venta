namespace PuntoVentaElectrimax.Models.Request
{
    public class ProductoRequest
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public float Descuento { get; set; }
    }
}
