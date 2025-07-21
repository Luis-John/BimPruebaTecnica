namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
public interface IDeleteLocalidadCommandRepository
{
    Task DeleteLocalidadAsync(int id);
}
