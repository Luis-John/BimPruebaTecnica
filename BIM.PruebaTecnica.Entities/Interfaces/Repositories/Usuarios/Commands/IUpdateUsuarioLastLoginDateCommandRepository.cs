namespace BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
public interface IUpdateUsuarioLastLoginDateCommandRepository
{
    Task UpdateUsuarioLastLoginDateAsync(string id);
}
