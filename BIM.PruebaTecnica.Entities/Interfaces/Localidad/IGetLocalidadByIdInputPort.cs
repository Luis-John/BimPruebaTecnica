using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Localidad;
public interface IGetLocalidadByIdInputPort
{
    Task<LocalidadByIdDto> GetLocalidadByIdAsync(int id);
}
