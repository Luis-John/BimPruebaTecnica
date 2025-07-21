using System.ComponentModel.DataAnnotations;

namespace BIM.PruebaTecnica.AppMVC.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Usuario")]
    [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
    [MaxLength(256, ErrorMessage = "El nombre no debe superar los 256 caracteres.")]
    public string NombreUsuario { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "El campo debe ser un correo electronico valido.")]
    [Display(Name = "Correo electronico")]
    public string Email { get; set; }

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
