using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Localidad;
public interface IUpdateLocalidadInputPort
{
    Task UpdateLocalidadAsync(UpdateLocalidadDto updateLocalidadDto);
}
