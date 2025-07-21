namespace BIM.PruebaTecnica.AppMVC.Models;

public class PaginacionDto
{
    public string BaseURL { get; set; }
    public int PaginaActual { get; set; } = 1;
    public int RegistrosPorPagina { get; set; } = 5;
    public int TotalPaginas { get; set; }
}

public class PaginacionDto<LocalidadByIdUserDto> : PaginacionDto 
{
    public List<LocalidadByIdUserDto> LstLocalidadByIdUser { get; set; }
}

