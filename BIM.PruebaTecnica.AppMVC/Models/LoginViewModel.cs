
using System.ComponentModel.DataAnnotations;

namespace BIM.PruebaTecnica.AppMVC.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Usuario")]
    public string NombreUsuario { get; set; }

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
