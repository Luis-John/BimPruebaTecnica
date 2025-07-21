using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Validations;
using Newtonsoft.Json;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class UpdateLocalidadInteractor(
    IUpdateLocalidadCommandRepository UpdateLocalidadRepository,
    IGetMunicipioByIdQueryRepository GetMunicipioByIdRepository,
    IGetUsuarioByIdQueryRepository GetUsuarioByIdRepository) : IUpdateLocalidadInputPort
{
    private readonly Log Log = new Log("UpdateLocalidadInteractor");
    public async Task UpdateLocalidadAsync(UpdateLocalidadDto localidad)
    {
        try
        {
            if (new LocalidadValidations().ValidateId(localidad.Id))
                if (new LocalidadValidations().ValidateLocalidad(localidad.Descripcion))
                    if (new LocalidadValidations().ValidateCodigoPostal(localidad.CodigoPostal))
                        if (new LocalidadValidations().ValidateMunicipio(localidad.IdMunicipio))
                            if (new LocalidadValidations().ValidateIdUsuario(localidad.IdUsuario))
                                if (new LocalidadValidations().ValidateCodigoPostal(localidad.CodigoPostal))
                                {
                                    var user = await GetUsuarioByIdRepository.GetUsuarioByIdAsync(localidad.IdUsuario);
                                    if (string.IsNullOrWhiteSpace(user.NombreUsuario))
                                        throw new BadRequestException($"No existe el usuario con el identificador: {localidad.IdUsuario}");

                                    var municipio = await GetMunicipioByIdRepository.GetMunicipioAsync(localidad.IdMunicipio);
                                    if (municipio.Id == default)
                                        throw new BadRequestException($"No existe el municipio con el identificador: {localidad.IdMunicipio}");

                                    Entities.POCOEntities.Localidad resultTmp = new Entities.POCOEntities.Localidad()
                                    {
                                        Id = localidad.Id,
                                        Descripcion = localidad.Descripcion,
                                        CodigoPostal = localidad.CodigoPostal,
                                        IdMunicipio = localidad.IdMunicipio,
                                        IdUsuario = localidad.IdUsuario
                                    };
                                    await UpdateLocalidadRepository.UpdateLocalidadAsync(resultTmp);
                                }
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae, JsonConvert.SerializeObject(localidad)); throw iae; }
        catch (Exception ex) { Log.LogError(ex, JsonConvert.SerializeObject(localidad)); throw new InternalApiException("Error no controlado Crear Localidad", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.UpdateLocalidadInteractor.UpdateLocalidadAsync()"); }
    }
}
