using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BIM.PruebaTecnica.AppMVC.Models;

public class LocalidadViewModel
{
    public int? Id { get; set; }

    [Display(Name = "Estado")]
    [Required(ErrorMessage = "El Seleccione un {0}")]
    public int? IdEstado { get; set; }

    [Display(Name = "Municipio")]
    [Required(ErrorMessage = "Seleccione un {0}.")]
    public int? IdMunicipio { get; set; }

    [Display(Name = "Descripcion de localidad")]
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [MinLength(5, ErrorMessage = "El campo {0} debe tener al menos 5 caracteres.")]
    [MaxLength(500, ErrorMessage = "El campo {0} debe tener menos de 500 caracteres.")]
    public string Descripcion { get; set; }

    [Display(Name = "Codigo Postal")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se permiten números.")]
    public int CodigoPostal { get; set; }


    public List<SelectListItem>? Estados { get; set; }
    public List<SelectListItem>? Municipios { get; set; }

}
