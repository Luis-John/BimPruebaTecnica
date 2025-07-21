using BIM.PruebaTecnica.Entities.Options;
using BIM.PruebaTecnica.Presenters;
using BIM.PruebaTecnica.Repository;
using BIM.PruebaTecnica.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BIM.PruebaTecnica.IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddBimServices(this IServiceCollection services,
        Action<DBOptions> dbOptions,Action<AesOptions> aesOptions)
    {        
        services.AddUseCasesServices(aesOptions);
        services.AddRepositoryServices(dbOptions);
        services.AddPresentersServices();
        


        return services;
    }
}