using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.UseCases.Validations;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class GetLocalidadByIdUserInteractor(
    IGetLocalidadByIdUserQueryRepository GetLocalidadByIdUserRepositorym,
    IGetUsuarioByIdQueryRepository GetUsuarioByIdRepository) : IGetLocalidadByIdUserInputPort
{
    public async Task<IEnumerable<LocalidadByIdUserDto>> GetLocalidadByIdUserAsync(string idUsuario, int pagina, int registroPorPagina)
    {
        IEnumerable<LocalidadByIdUserDto> lstResultTmp = new List<LocalidadByIdUserDto>();
        try
        {
            if (new LocalidadValidations().ValidateLocalidad(idUsuario))
            {
                var usuarioDb = await GetUsuarioByIdRepository.GetUsuarioByIdAsync(idUsuario);
                if (string.IsNullOrWhiteSpace(usuarioDb.NombreUsuario))
                    throw new BadRequestException($"No existe el usuario con el identificador: {idUsuario}.");

                lstResultTmp = await GetLocalidadByIdUserRepositorym.GetLocalidadByIdUserAsync(idUsuario, pagina, registroPorPagina);
            }
        }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { throw iae; }
        catch (Exception ex) { throw new InternalApiException("Error no controlado obtener Localidad por idusuario", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.GetLocalidadByIdUserInteractor.GetLocalidadByIdUserAsync()"); }
        return lstResultTmp;
    }
}
