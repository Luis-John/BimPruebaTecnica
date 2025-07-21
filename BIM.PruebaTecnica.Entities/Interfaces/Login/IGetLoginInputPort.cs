namespace BIM.PruebaTecnica.Entities.Interfaces.Login;
public interface IGetLoginInputPort
{
    Task LoginAsync(string user, string password);
}
