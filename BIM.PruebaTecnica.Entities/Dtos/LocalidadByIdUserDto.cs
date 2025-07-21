namespace BIM.PruebaTecnica.Entities.Dtos;
public class LocalidadByIdUserDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public DateTime CreateDate { get; set; }
    public string Municipio { get; set; }
    public string Estado { get; set; }
}

