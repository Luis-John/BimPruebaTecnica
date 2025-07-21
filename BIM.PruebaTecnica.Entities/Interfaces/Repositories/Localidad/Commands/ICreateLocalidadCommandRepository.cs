namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
public interface ICreateLocalidadCommandRepository
{
    Task CreateLocalidadAsync(POCOEntities.Localidad localidad);
}

