using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Usuarios;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Validations;
using Newtonsoft.Json;

namespace BIM.PruebaTecnica.UseCases.Usuarios;
internal class CreateUsuarioInteractor(
    ICreateUsuarioCommandRepository repository,
    IGetUsuarioByIdQueryRepository GetUsuarioByIdRepository,
    IGetUsuarioByEmailQueryRepository GetUsuarioByEmailRepository) : ICreateUsuarioInputPort
{
    private readonly Log Log = new Log("CreateUsuarioInteractor");
    public async Task CreateUsuarioAsyn(UsuariosDto usuario)
    {
        try
        {
            if (new UsuariosValidations().ValidateNombreUsuario(usuario.NombreUsuario))
                if (new UsuariosValidations().ValidateEmail(usuario.Email))
                    if (new UsuariosValidations().ValidatePassword(usuario.Password))
                    {
                        var usuarioDB = await GetUsuarioByIdRepository.GetUsuarioByIdAsync(usuario.NombreUsuario);
                        if (!string.IsNullOrWhiteSpace(usuarioDB.NombreUsuario))
                            throw new BadRequestException($"El nombre de usuario: {usuario.NombreUsuario} ya se encuentra en uso.");

                        var usuarioEmailDB = await GetUsuarioByEmailRepository.GetUsuarioByEmailAsync(usuario.Email);
                        if (!string.IsNullOrWhiteSpace(usuarioEmailDB.Email))
                            throw new BadRequestException($"Favor de utilizar otro correo electronico.");

                        Entities.POCOEntities.Usuarios result = new Entities.POCOEntities.Usuarios()
                        {
                            NombreUsuario = usuario.NombreUsuario,
                            Email = usuario.Email,
                            Password = usuario.Password
                        };

                        await repository.CreateUsuarioAsync(result);
                    }
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae, JsonConvert.SerializeObject(usuario)); throw iae; }
        catch (Exception ex) { Log.LogError(ex, JsonConvert.SerializeObject(usuario)); throw new InternalApiException("Error no controlado create usuario", ex.Message, "BIM.PruebaTecnica.UseCases.Usuarios.CreateUsuarioInteractor.CreateUsuarioAsyn()"); }
    }
}
