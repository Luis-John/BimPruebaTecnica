namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
public interface IGetUsuarioByIdQueryRepository
{
    Task<POCOEntities.Usuarios> GetUsuarioByIdAsync(string idUsuario);
}
