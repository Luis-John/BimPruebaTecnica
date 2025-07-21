using BIM.PruebaTecnica.Entities.Dtos;

namespace BIM.PruebaTecnica.Entities.Interfaces.Login;
public interface IGetLoginOutputPort
{
    TokenDto Token { get; }
    Task GetLoginAsync(TokenDto tokenDto);
}
