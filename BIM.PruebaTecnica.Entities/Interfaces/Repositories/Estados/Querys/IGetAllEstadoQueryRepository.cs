namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
public interface IGetAllEstadoQueryRepository
{
    Task<IEnumerable<POCOEntities.Estados>> GetAllEstadosAsync();
}
