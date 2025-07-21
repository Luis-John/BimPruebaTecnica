using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Localidad;
public interface ICreateLocalidadInputPort
{
    Task CreateLocalidadAsync(LocalidadDto localidad);
}
