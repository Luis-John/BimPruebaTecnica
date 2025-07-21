using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Estados;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.UseCases.Helper;
using Newtonsoft.Json;

namespace BIM.PruebaTecnica.UseCases.Estados;
internal class GetAllEstadosInteractor(IGetAllEstadoQueryRepository GetAllEstadoRepository) : IGetAllEstadosInputPort
{
    private readonly Log Log = new Log("GetAllEstadosInteractor");
    public async Task<IEnumerable<EstadosDto>> GetAllEstadosAsync()
    {
        try
        {
            var lstResultTmp = await GetAllEstadoRepository.GetAllEstadosAsync();
            return JsonConvert.DeserializeObject<List<EstadosDto>>(JsonConvert.SerializeObject(lstResultTmp));
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { Log.LogError(iae); throw iae; }
        catch (Exception ex) { Log.LogError(ex); throw new InternalApiException("Error no controlado obtener estados", ex.Message, "BIM.PruebaTecnica.UseCases.Estados.GetAllEstadosInteractor.GetAllEstadosAsync()"); }
    }
}
