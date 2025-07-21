using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class DeleteLocalidadInteractor(
    IDeleteLocalidadCommandRepository DeleteLocalidadRepository,
    IGetLocalidadByIdQueryRepository GetLocalidadByIdRepository) : IDeleteLocalidadInputPort
{
    public async Task DeleteLocalidadAsync(int id)
    {
        try
        {
            var result = await GetLocalidadByIdRepository.GetLocalidadByIdAsync(id);
            if (result.Id == default)
                throw new BadRequestException($"No existe la localidad con el identificador: {id}.");

            await DeleteLocalidadRepository.DeleteLocalidadAsync(id);
        }
        catch (UnauthorizationException ue) { throw ue; }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { throw iae; }
        catch (Exception ex) { throw new InternalApiException("Error no controlado al eliminar Localidad", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.DeleteLocalidadInteractor.DeleteLocalidadAsync()"); }
    }
}
