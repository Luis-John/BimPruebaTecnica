using BIM.PruebaTecnica.Entities.Interfaces.Estados;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Login;
using BIM.PruebaTecnica.Entities.Interfaces.Municipios;
using BIM.PruebaTecnica.Entities.Interfaces.Usuarios;
using BIM.PruebaTecnica.Entities.Options;
using BIM.PruebaTecnica.UseCases.Estados;
using BIM.PruebaTecnica.UseCases.Helper;
using BIM.PruebaTecnica.UseCases.Localidad;
using BIM.PruebaTecnica.UseCases.Login;
using BIM.PruebaTecnica.UseCases.Municipios;
using BIM.PruebaTecnica.UseCases.Usuarios;
using Microsoft.Extensions.DependencyInjection;

namespace BIM.PruebaTecnica.UseCases;

public static class DependencyContainer
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services,
        Action<AesOptions> aesOptions)
    {
        services.Configure(aesOptions);
        services.AddScoped<ICreateTokenHelper, CreateTokenHelper>();
        services.AddScoped<ICreateUsuarioInputPort, CreateUsuarioInteractor>();     
        services.AddScoped<IGetLoginInputPort, GetLoginInteractor>();     
        services.AddScoped<ICreateLocalidadInputPort, CreateLocalidadInteractor>();     
        services.AddScoped<IGetAllEstadosInputPort, GetAllEstadosInteractor>();     
        services.AddScoped<IGetMunicipioByIdEstadoInputPort, GetMunicipioByIdEstadoInteractor>();     
        services.AddScoped<IGetLocalidadByIdUserInputPort, GetLocalidadByIdUserInteractor>();     
        services.AddScoped<IGetLocalidadTotalPaginasInputPort, GetLocalidadTotalPaginasInteractor>();     
        services.AddScoped<IGetLocalidadByIdInputPort, GetLocalidadByIdInteractor>();     
        services.AddScoped<IUpdateLocalidadInputPort, UpdateLocalidadInteractor>();     
        services.AddScoped<IDeleteLocalidadInputPort, DeleteLocalidadInteractor>();     

        return services;
    }
}