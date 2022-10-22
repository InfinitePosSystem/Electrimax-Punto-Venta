using PuntoVentaElectrimax.Models.Request;
using PuntoVentaElectrimax.Models.Response;

namespace PuntoVentaElectrimax.Servicios
{
    public interface IUserService 
    {
        UserResponse Auth(AuthRequest model);
    }
}
