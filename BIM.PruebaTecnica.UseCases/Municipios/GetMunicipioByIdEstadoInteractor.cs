using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Municipios;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Validations;
using Newtonsoft.Json;

namespace BIM.PruebaTecnica.UseCases.Municipios;
internal class GetMunicipioByIdEstadoInteractor(
    IGetEstadoByIdQueryRepository GetEstadoByIdRepository,
    IGetMunicipioByIdEstadoQueryRepository GetMunicipioByIdEstadoRepository
    ) : IGetMunicipioByIdEstadoInputPort
{
    private readonly Log Log = new Log("GetMunicipioByIdEstadoInteractor");
    public async Task<IEnumerable<MunicipiosDto>> GetMunicipiosByIdEstadoAsync(int idEstado)
    {
        string parametros = $"idEstado:{idEstado}";
        try
        {
            List<MunicipiosDto> lstResult = new List<MunicipiosDto>();
            if (new LocalidadValidations().ValidateIdEstado(idEstado))
            {
                var estadoDb = await GetEstadoByIdRepository.GetEstadoByIdAsync(idEstado);
                if (estadoDb.Id == default)
                    throw new BadRequestException($"No existe identificador de estado: {idEstado}.");

                var lstResultTmp = await GetMunicipioByIdEstadoRepository.GetMunicipioByIdEstadoAsync(idEstado);
                lstResult = JsonConvert.DeserializeObject<List<MunicipiosDto>>(JsonConvert.SerializeObject(lstResultTmp));
            }
            return lstResult;
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae, parametros); throw iae; }
        catch (Exception ex) { Log.LogError(ex, parametros); throw new InternalApiException("Error no controlado Obtener estados por id estado", ex.Message, "BIM.PruebaTecnica.UseCases.Municipios.GetMunicipioByIdEstadoInteractor.GetMunicipiosByIdEstadoAsync()"); }
    }
}
