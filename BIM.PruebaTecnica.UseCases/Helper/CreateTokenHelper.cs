using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BIM.PruebaTecnica.UseCases.Helper;
internal class CreateTokenHelper : ICreateTokenHelper
{
    public async Task<string> CreateTokenAsync(string usuario)
    {

        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890"));

        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "BIM.com",
            audience: "BIM.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credenciales
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
