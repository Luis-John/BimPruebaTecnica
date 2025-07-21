namespace BIM.PruebaTecnica.Entities.POCOEntities;
public class Usuarios
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public string UserUpdate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DateTime LastPassword { get; set; }
}
