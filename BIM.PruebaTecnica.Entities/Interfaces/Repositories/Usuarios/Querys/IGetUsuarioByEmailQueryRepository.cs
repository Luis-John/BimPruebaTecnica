namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
public interface IGetUsuarioByEmailQueryRepository
{
    Task<POCOEntities.Usuarios> GetUsuarioByEmailAsync(string email);
}
