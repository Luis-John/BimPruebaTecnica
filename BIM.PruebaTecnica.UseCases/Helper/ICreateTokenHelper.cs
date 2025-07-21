namespace BIM.PruebaTecnica.UseCases.Helper;
public interface ICreateTokenHelper
{
    Task<string> CreateTokenAsync(string usuario);
}
