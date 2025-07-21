using BIM.PruebaTecnica.Entities.Exceptions;
using System.Text.RegularExpressions;

namespace BIM.PruebaTecnica.UseCases.Validations;
public class UsuariosValidations
{
    public UsuariosValidations()
    {
    }

    public bool ValidateNombreUsuario(string nombreUsuario)
    {
        if (string.IsNullOrEmpty(nombreUsuario))
            throw new BadRequestException("El nombre de usuario es requerido.");

        if (nombreUsuario.Length < 5)
            throw new BadRequestException("El nombre de usuario debe tener al menos 5 caracteres.");

        if (!Regex.IsMatch(nombreUsuario, @"^[^\s]+$"))
            throw new BadRequestException("El nombre de usuario no debe contener espacios.");

        return true;
    }

    public bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new BadRequestException("El correo electronico es requerido.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new BadRequestException("El correo electronico debe ser un correo electronico valido.");

        return true;
    }

    public bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new BadRequestException("La contraseña de usuario es requerido.");

        if (password.Length < 6)
            throw new BadRequestException("La contraseña de usuario debe tener al menos 6 caracteres.");

        return true;
    }
}
