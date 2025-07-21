using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
public interface IGetLocalidadByIdUserQueryRepository
{
    Task<IEnumerable<LocalidadByIdUserDto>> GetLocalidadByIdUserAsync(string idUsuario, int pagina, int registroPorPagina);
}
