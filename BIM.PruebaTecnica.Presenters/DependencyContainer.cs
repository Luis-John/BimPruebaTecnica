using BIM.PruebaTecnica.Entities.Interfaces.Login;
using Microsoft.Extensions.DependencyInjection;

namespace BIM.PruebaTecnica.Presenters;

public static class DependencyContainer
{
    public static IServiceCollection AddPresentersServices(this IServiceCollection services)
    {
        services.AddScoped<IGetLoginOutputPort, GetLoginPresenter>();
        return services;
    }
}