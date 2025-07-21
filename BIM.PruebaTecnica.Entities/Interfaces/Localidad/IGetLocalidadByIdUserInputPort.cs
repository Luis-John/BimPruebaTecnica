using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Localidad;
public interface IGetLocalidadByIdUserInputPort
{
    Task<IEnumerable<LocalidadByIdUserDto>> GetLocalidadByIdUserAsync(string IdUsuario, int pagina, int registroPorPagina);
}
