using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.UseCases.Validations;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class GetLocalidadTotalPaginasInteractor(
    IGetLocalidadTotalPaginasQueryRepository GetLocalidadTotalPaginasRepository
    ) : IGetLocalidadTotalPaginasInputPort
{
    public async Task<int> GetLocalidadTotalPaginasAsync(string idUsuario, int registrosPorPagina)
    {
        int result = default;
        try
        {
            if (new LocalidadValidations().ValidateLocalidad(idUsuario))
                result = await GetLocalidadTotalPaginasRepository.GetLocalidadTotalPaginasAsync(idUsuario, registrosPorPagina);
        }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { throw iae; }
        catch (Exception ex) { throw new InternalApiException("Error no controlado obtener Localidad Total Paginas", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.GetLocalidadTotalPaginasInteractor.GetLocalidadTotalPaginasAsync()"); }
        return result;
    }
}
