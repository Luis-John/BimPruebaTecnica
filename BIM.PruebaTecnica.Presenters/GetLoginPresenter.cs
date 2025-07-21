using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Interfaces.Login;

namespace BIM.PruebaTecnica.Presenters;
internal class GetLoginPresenter : IGetLoginOutputPort
{
    public TokenDto Token { get; set; }

    public Task GetLoginAsync(TokenDto tokenDto)
    {
        Token = tokenDto;
        return Task.CompletedTask;
    }
}
