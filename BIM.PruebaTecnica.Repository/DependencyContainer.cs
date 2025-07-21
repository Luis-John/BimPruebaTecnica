using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using BIM.PruebaTecnica.Repository.Estados.Querys;
using BIM.PruebaTecnica.Repository.Localidad.Commands;
using BIM.PruebaTecnica.Repository.Localidad.Querys;
using BIM.PruebaTecnica.Repository.Municipios.Querys;
using BIM.PruebaTecnica.Repository.Usuarios.Commands;
using BIM.PruebaTecnica.Repository.Usuarios.Querys;
using Microsoft.Extensions.DependencyInjection;

namespace BIM.PruebaTecnica.Repository;

public static class DependencyContainer
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services,
        Action<DBOptions> dbOptions)
    {
        services.AddScoped<ICreateUsuarioCommandRepository, CreateUsuarioSqlRepository>();
        services.AddScoped<IGetUsuarioByIdQueryRepository, GetUsuarioByIdSqlRepository>();
        services.AddScoped<IGetUsuarioByEmailQueryRepository, GetUsuarioByEmailSqlRepository>();
        services.AddScoped<IGetEstadoByIdQueryRepository, GetEstadoByIdSqlRepository>();
        services.AddScoped<IGetMunicipioByIdQueryRepository, GetMunicipioByIdSqlRepository>();
        services.AddScoped<IGetLocalidadByIdQueryRepository, GetLocalidadByIdSqlRepository>();
        services.AddScoped<ICreateLocalidadCommandRepository, CreateLocalidadSqlRepository>();
        services.AddScoped<IUpdateUsuarioLastLoginDateCommandRepository, UpdateUsuarioLastLoginDateSqlRepository>();
        services.AddScoped<IGetMunicipioByIdEstadoQueryRepository, GetMunicipioByIdEstadoSqlRepository>();
        services.AddScoped<IGetAllEstadoQueryRepository, GetAllEstadoSqlRepository>();
        services.AddScoped<IGetLocalidadByIdUserQueryRepository, GetLocalidadByIdUserSqlRepository>();
        services.AddScoped<IGetLocalidadTotalPaginasQueryRepository, GetLocalidadTotalPaginasSqlRepository>();
        services.AddScoped<IUpdateLocalidadCommandRepository, UpdateLocalidadSqlRepository>();
        services.AddScoped<IDeleteLocalidadCommandRepository, DeleteLocalidadSqlRepository>();

        services.Configure(dbOptions);

        return services;
    }
}