namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
public interface IGetMunicipioByIdEstadoQueryRepository
{
    Task<IEnumerable<POCOEntities.Municipios>> GetMunicipioByIdEstadoAsync(int idEstado);
}
