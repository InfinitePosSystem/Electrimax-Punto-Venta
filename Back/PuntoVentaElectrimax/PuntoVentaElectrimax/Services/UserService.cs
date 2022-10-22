using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PuntoVentaElectrimax.Models;
using PuntoVentaElectrimax.Models.Common;
using PuntoVentaElectrimax.Models.Request;
using PuntoVentaElectrimax.Models.Response;
using PuntoVentaElectrimax.tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PuntoVentaElectrimax.Servicios
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSetings)
        {
            _appSettings = appSetings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            
            UserResponse userresponse = new UserResponse();
            using (var db = new localdb_electrimaxContext())
            {
                string spassword = Encrypt.GetSHA256(model.Contraseña);
                var usuario = db.Usuarios.Where(d=> d.Usuario1 == model.Usuario &&
                                                d.Contraseña == spassword).FirstOrDefault();
                if (usuario == null) return null;
                userresponse.Usuario = usuario.Usuario1;
                userresponse.Token = GetToken(usuario);
            }
            return userresponse;
        }
        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Usuario1)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
