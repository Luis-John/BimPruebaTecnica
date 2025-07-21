using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Usuarios.Commands;

internal class UpdateUsuarioLastLoginDateSqlRepository(IOptions<DBOptions> options) : IUpdateUsuarioLastLoginDateCommandRepository
{
    public async Task UpdateUsuarioLastLoginDateAsync(string id)
    {
        try
        {
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(options.Value.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[ApiUsuario].[UpdateUsuarioLastLoginDate]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = id;
                    cmd.CommandTimeout = 120;
                    int r = cmd.ExecuteNonQuery();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Commands.UpdateUsuarioLastLoginDateSqlRepository.UpdateUsuarioLastLoginDateAsync() => [ApiUsuario].[UpdateUsuarioLastLoginDate]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Commands.UpdateUsuarioLastLoginDateSqlRepository.UpdateUsuarioLastLoginDateAsync()"); }
    }
}
