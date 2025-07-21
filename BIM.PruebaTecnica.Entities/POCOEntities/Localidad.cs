namespace BIM.PruebaTecnica.Entities.POCOEntities;
public class Localidad
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int CodigoPostal { get; set; }
    public int IdMunicipio { get; set; }
    public string IdUsuario { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UserUpdate { get; set; }
}

