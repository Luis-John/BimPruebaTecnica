namespace BIM.PruebaTecnica.Entities.Exceptions;
public class InternalApiException : Exception
{
    public int StatusCode { get; set; } = 500;
    public string Mensaje { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
    public string Metodo { get; set; } = string.Empty;


    public InternalApiException(string message) : base(message)
    {
        this.Mensaje = message;
    }

    public InternalApiException(string message, string detalle, string metodo) : base(message)
    {
        this.Mensaje = message;
        this.Detalle = detalle;
        this.Metodo = metodo;
    }
}
