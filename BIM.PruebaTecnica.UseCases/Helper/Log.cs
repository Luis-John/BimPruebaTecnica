using BIM.PruebaTecnica.Entities.Exceptions;
using System.Text;

namespace BIM.PruebaTecnica.UseCases.Helper;
public class Log
{
    private string Path = string.Empty;
    private string Name = string.Empty;
    public Log(string Name)
    {
        this.Name = Name;
        InitiValues();
    }

    private void InitiValues()
    {
        if (string.IsNullOrWhiteSpace(this.Path))
            this.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        if (!Directory.Exists(Path))
            Directory.CreateDirectory(Path);
    }

    public void LogError(InternalApiException exception, string parametros = "")
    {
        StringBuilder message = new StringBuilder();
        try
        {
            message.AppendLine("");
            message.AppendLine("");
            if (!string.IsNullOrWhiteSpace(exception.Source))
                message.AppendLine($"Aplicación: {exception.Source.Trim()}");
            if (exception.TargetSite != null)
                message.AppendLine($"Metodo: {exception.Metodo.ToString()}");
            if (!string.IsNullOrWhiteSpace(parametros))
                message.AppendLine($"Parametros: {parametros.Trim()}");
            if (!string.IsNullOrWhiteSpace(exception.Message))
                message.AppendLine($"Mensaje: {exception.Message.Trim()}");
            if (!string.IsNullOrWhiteSpace(exception.Detalle))
                message.AppendLine($"Detalle: {exception.Detalle.Trim()}");
            LogError(message.ToString());
        }
        catch (Exception ex) { throw new ArgumentException($"MethodName(LogError): {ex.Message}, {(ex.StackTrace == null ? string.Empty : ex.StackTrace)} "); }
    }

    public void LogError(Exception exception, string parametros = "")
    {
        StringBuilder message = new StringBuilder();
        try
        {
            message.AppendLine("");
            message.AppendLine("");
            if (!string.IsNullOrWhiteSpace(exception.Source))
                message.AppendLine($"Aplicación: {exception.Source.Trim()}");
            if (exception.TargetSite != null)
                message.AppendLine($"Metodo: {exception.TargetSite.ToString()}");
            if (!string.IsNullOrWhiteSpace(parametros))
                message.AppendLine($"Parametros: {parametros.Trim()}");
            if (!string.IsNullOrWhiteSpace(exception.Message))
                message.AppendLine($"Mensaje: {exception.Message.Trim()}");
            LogError(message.ToString());
        }
        catch (Exception ex) { throw new ArgumentException($"MethodName(LogError): {ex.Message}, {(ex.StackTrace == null ? string.Empty : ex.StackTrace)} "); }
    }

    public void LogError(string message)
    {
        try
        {
            StringBuilder ErrorSBuilder = new StringBuilder();
            ErrorSBuilder = new StringBuilder();
            ErrorSBuilder.AppendLine("=========================================================================");
            ErrorSBuilder.AppendLine($"{DateTime.Now.ToString("hh:mm:ss:ff")}\r\n{message.Trim(Environment.NewLine.ToArray())}");
            ErrorSBuilder.AppendLine("=========================================================================");

            string LogName = string.Format($"Log_Error_{this.Name.Trim()}{DateTime.Now.ToString("yyyyMMdd")}.txt");
            lock (this)
            {
                using (StreamWriter swLog = new StreamWriter(System.IO.Path.Combine(this.Path, LogName), true, Encoding.UTF8))
                    swLog.Write(ErrorSBuilder.ToString());
            }
        }
        catch (Exception ex) { throw new ArgumentException($"MethodName(LogError): {0}", ex.Message); }
    }
}
