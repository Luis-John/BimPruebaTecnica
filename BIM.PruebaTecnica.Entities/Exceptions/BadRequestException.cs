namespace BIM.PruebaTecnica.Entities.Exceptions;
public class BadRequestException : Exception
{

    public int StatusCode { get; set; } = 400;
    public string Mensaje { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
    public string Metodo { get; set; } = string.Empty;

    public BadRequestException(string message) : base(message)
    {
        this.Mensaje = message;
    }

    public BadRequestException(string message, string detalle, string metodo) : base(message)
    {
        this.Mensaje = message;
        this.Detalle = detalle;
        this.Metodo = metodo;
    }

}
