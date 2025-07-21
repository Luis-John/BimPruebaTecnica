namespace BIM.PruebaTecnica.Entities.Dtos;
public class LocalidadByIdDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public int IdMunicipio { get; set; }
    public string Municipio { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdEstado { get; set; }
    public string Estado { get; set; }
}
