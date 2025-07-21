namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
public interface IGetEstadoByIdQueryRepository
{
    Task<POCOEntities.Estados> GetEstadoByIdAsync(int id);
}
