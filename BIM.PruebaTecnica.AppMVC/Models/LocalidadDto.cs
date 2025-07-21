namespace BIM.PruebaTecnica.AppMVC.Models;

public class LocalidadDto
{
    public string? Id { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public int IdMunicipio { get; set; }
    public string? Municipio { get; set; }
    public int IdEstado { get; set; }
    public string? Estado { get; set; }
    public string IdUsuario { get; set; }
}
