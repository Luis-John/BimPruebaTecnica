using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Usuarios;
public interface ICreateUsuarioInputPort
{
    Task CreateUsuarioAsyn(UsuariosDto usuario);
}
