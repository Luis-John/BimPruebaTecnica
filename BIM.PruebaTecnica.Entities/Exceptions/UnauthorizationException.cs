namespace BIM.PruebaTecnica.Entities.Exceptions;
public class UnauthorizationException : Exception
{

    public int StatusCode { get; set; } = 401;
    public string Mensaje { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;

    public UnauthorizationException(string message) : base(message)
    {
        this.Mensaje = message;
    }

 
}
