using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Municipios;
public interface IGetMunicipioByIdEstadoInputPort
{
    Task<IEnumerable<MunicipiosDto>> GetMunicipiosByIdEstadoAsync(int idEstado);
}
