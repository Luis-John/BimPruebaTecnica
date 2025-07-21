using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.UseCases.Validations;

namespace BIM.PruebaTecnica.UseCases.Localidad;
internal class GetLocalidadByIdInteractor(
    IGetLocalidadByIdQueryRepository GetLocalidadByIdRepository) : IGetLocalidadByIdInputPort
{
    public async Task<LocalidadByIdDto> GetLocalidadByIdAsync(int id)
    {
        LocalidadByIdDto result = new LocalidadByIdDto();
        try
        {
            if (new LocalidadValidations().ValidateId(id))
            {
                result = await GetLocalidadByIdRepository.GetLocalidadByIdAsync(id);
            }
        }
        catch (BadRequestException bre) { throw bre; }
        catch (InternalApiException iae) { throw iae; }
        catch (Exception ex) { throw new InternalApiException("Error no controlado obtener Localidad por id", ex.Message, "BIM.PruebaTecnica.UseCases.Localidad.GetLocalidadByIdInteractor.GetLocalidadByIdAsync()"); }
        return result;
    }
}
