namespace BIM.PruebaTecnica.Entities.Dtos;
public class UpdateLocalidadDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public int IdMunicipio { get; set; }
    public string IdUsuario { get; set; }
}
