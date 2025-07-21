using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Login;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Validations;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace BIM.PruebaTecnica.UseCases.Login;
internal class GetLoginInteractor(
    IGetUsuarioByIdQueryRepository UsuarioByIdRepository,
    IGetLoginOutputPort LoginOutputPort,
    ICreateTokenHelper createToken,
    IUpdateUsuarioLastLoginDateCommandRepository UpdateUsuarioLastLoginDateRepository,
    IOptions<AesOptions> AesOptions
    ) : IGetLoginInputPort
{
    private readonly Log Log = new Log("GetLoginInteractor");
    public async Task LoginAsync(string user, string password)
    {
        string parametros = $"user:{user}, password:{password}";
        try
        {
            TokenDto result = new TokenDto();
            if (new UsuariosValidations().ValidateNombreUsuario(user))
                if (new UsuariosValidations().ValidatePassword(password))
                {
                    var usuarioDB = await UsuarioByIdRepository.GetUsuarioByIdAsync(user);
                    if (string.IsNullOrWhiteSpace(usuarioDB.NombreUsuario))
                        throw new BadRequestException("Las credenciales proporcionadas son incorrectas");

                    if (!usuarioDB.Active)
                        throw new UnauthorizationException("Acceso denegado");

                    if (Desencriptar(password) != Desencriptar(usuarioDB.Password))
                        throw new BadRequestException("Las credenciales proporcionadas son incorrectas");

                    result.Token = await createToken.CreateTokenAsync(user);

                    await UpdateUsuarioLastLoginDateRepository.UpdateUsuarioLastLoginDateAsync(user);
                    await LoginOutputPort.GetLoginAsync(result);
                }
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae, parametros); throw iae; }
        catch (Exception ex) { Log.LogError(ex, parametros); throw new InternalApiException("Error no controlado Login", ex.Message, "BIM.PruebaTecnica.UseCases.Login.GetLoginInteractor.LoginAsync()"); }
    }

    #region Desencriptar
    public string Desencriptar(string cifrado)
    {
        try
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(AesOptions.Value.Clave);
            aes.IV = Encoding.UTF8.GetBytes(AesOptions.Value.AesIV);

            using var decryptor = aes.CreateDecryptor();
            byte[] buffer = Convert.FromBase64String(cifrado);
            byte[] resultado = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(resultado);
        }
        catch (Exception ex) { throw new BadRequestException("Las credenciales proporcionadas son incorrectas"); }
    }
    #endregion

}
