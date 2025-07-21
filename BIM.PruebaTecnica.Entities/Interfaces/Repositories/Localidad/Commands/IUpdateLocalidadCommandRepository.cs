namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
public interface IUpdateLocalidadCommandRepository
{
    Task UpdateLocalidadAsync(POCOEntities.Localidad localidad);
}
