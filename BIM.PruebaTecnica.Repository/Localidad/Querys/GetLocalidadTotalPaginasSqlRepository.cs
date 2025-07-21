using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Localidad.Querys;
internal class GetLocalidadTotalPaginasSqlRepository(IOptions<DBOptions> options) : IGetLocalidadTotalPaginasQueryRepository
{
    public async Task<int> GetLocalidadTotalPaginasAsync(string idUsuario, int registrosPorPagina)
    {
        int result = default;
        try
        {
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiEstados].[LocalidadTotalPaginas]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.VarChar).Value = idUsuario;
                    cmd.Parameters.Add("@RegistrosPorPagina", SqlDbType.VarChar).Value = registrosPorPagina;
                    cmd.CommandTimeout = 120;
                    result = (int)cmd.ExecuteScalar();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Localidad.Querys.GetLocalidadTotalPaginasSqlRepository.GetLocalidadTotalPaginasAsync() => [ApiEstados].[LocalidadTotalPaginas]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Localidad.Querys.GetLocalidadTotalPaginasSqlRepository.GetLocalidadTotalPaginasAsync()"); }
        return result;
    }
}
