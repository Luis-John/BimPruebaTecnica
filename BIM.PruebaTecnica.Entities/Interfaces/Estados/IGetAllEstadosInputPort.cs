using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Estados;
public interface IGetAllEstadosInputPort
{
    Task<IEnumerable<EstadosDto>> GetAllEstadosAsync();
}
