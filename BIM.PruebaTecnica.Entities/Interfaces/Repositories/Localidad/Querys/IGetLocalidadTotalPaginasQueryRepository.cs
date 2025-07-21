namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
public interface IGetLocalidadTotalPaginasQueryRepository
{
    Task<int> GetLocalidadTotalPaginasAsync(string idUsuario, int registrosPorPagina);
}
