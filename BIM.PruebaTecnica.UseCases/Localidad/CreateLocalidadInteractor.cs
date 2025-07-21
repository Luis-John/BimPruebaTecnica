using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Validations;
using Newtonsoft.Json;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class CreateLocalidadInteractor(
    IGetMunicipioByIdQueryRepository GetMunicipioByIdRepository,
    ICreateLocalidadCommandRepository CreateLocalidadRepository,
    IGetUsuarioByIdQueryRepository GetUsuarioByIdRepository
    ) : ICreateLocalidadInputPort
{
    private readonly Log Log = new Log("CreateLocalidadInteractor");
    public async Task CreateLocalidadAsync(LocalidadDto localidad)
    {
        try
        {
            if (new LocalidadValidations().ValidateMunicipio(localidad.IdMunicipio))
                if (new LocalidadValidations().ValidateLocalidad(localidad.Descripcion))
                    if (new LocalidadValidations().ValidateCodigoPostal(localidad.CodigoPostal))
                    {
                        var municipioDb = await GetMunicipioByIdRepository.GetMunicipioAsync(localidad.IdMunicipio);
                        if (municipioDb.Id== default)
                            throw new BadRequestException($"No existe el municipio con el identificador: {localidad.IdMunicipio}.");

                        var usuarioDB = await GetUsuarioByIdRepository.GetUsuarioByIdAsync(localidad.IdUsuario);
                        if (string.IsNullOrWhiteSpace(usuarioDB.NombreUsuario))
                            throw new BadRequestException($"No existe el usuario con el identificador: {localidad.IdUsuario}.");

                        var resultTmp = JsonConvert.DeserializeObject<Entities.POCOEntities.Localidad>(JsonConvert.SerializeObject(localidad));
                        await CreateLocalidadRepository.CreateLocalidadAsync(resultTmp);
                    }
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae, JsonConvert.SerializeObject(localidad)); throw iae; }
        catch (Exception ex) { Log.LogError(ex, JsonConvert.SerializeObject(localidad)); throw new InternalApiException("Error no controlado Crear Localidad", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.CreateLocalidadInteractor.CreateLocalidadAsync()"); }
    }
}
