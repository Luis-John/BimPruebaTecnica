using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
public interface IGetLocalidadByIdQueryRepository
{
    Task<LocalidadByIdDto> GetLocalidadByIdAsync(int id);
}
