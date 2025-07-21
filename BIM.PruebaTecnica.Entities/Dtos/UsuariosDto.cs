namespace BIM.PruebaTecnica.Entities.Dtos;
public class UsuariosDto(string nombreUsuario, string email, string password)
{
    public string NombreUsuario => nombreUsuario;
    public string Email => email;
    public string Password => password;
}
