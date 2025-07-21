namespace BIM.PruebaTecnica.AppMVC.Models;

public class LocalidadByIdUserDto
{
    public int Id { get; set; }
    public int? IdEstado { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Municipio { get; set; }
    public string Estado { get; set; }
    public int? IdMunicipio { get; set; }
}
