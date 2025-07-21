using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Localidad.Commands;
internal class DeleteLocalidadSqlRepository(IOptions<DBOptions> options) : IDeleteLocalidadCommandRepository
{
    public async Task DeleteLocalidadAsync(int id)
    {
        try
        {
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(options.Value.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[ApiLocalidad].[DeleteLocalidad]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.CommandTimeout = 120;
                    int r = cmd.ExecuteNonQuery();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Localidad.Commands.DeleteLocalidadSqlRepository.DeleteLocalidadAsync() => [ApiLocalidad].[DeleteLocalidad]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Localidad.Commands.DeleteLocalidadSqlRepository.DeleteLocalidadAsync()"); }
    }
}
