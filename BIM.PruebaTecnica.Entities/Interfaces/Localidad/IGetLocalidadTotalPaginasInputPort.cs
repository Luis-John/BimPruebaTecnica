using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Localidad;
public interface IGetLocalidadTotalPaginasInputPort
{
    Task<int> GetLocalidadTotalPaginasAsync(string idUsuario, int registrosPorPagina);
}
