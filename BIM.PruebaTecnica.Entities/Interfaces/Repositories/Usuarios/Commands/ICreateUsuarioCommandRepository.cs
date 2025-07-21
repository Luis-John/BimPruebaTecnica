namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
public interface ICreateUsuarioCommandRepository
{
    Task CreateUsuarioAsync(POCOEntities.Usuarios usuario);
}
