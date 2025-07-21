namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
public interface IGetMunicipioByIdQueryRepository
{
    Task<POCOEntities.Municipios> GetMunicipioAsync(int id);
}
