using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Commands;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Usuarios.Commands;
internal class CreateUsuarioSqlRepository(IOptions<DBOptions> options) : ICreateUsuarioCommandRepository
{
    public async Task CreateUsuarioAsync(Entities.POCOEntities.Usuarios usuario)
    {
        try
        {
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(options.Value.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[ApiUsuario].[CreateUsuario]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = usuario.NombreUsuario;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = usuario.Password;
                    cmd.CommandTimeout = 120;
                    int r = cmd.ExecuteNonQuery();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Commands.CreateUsuarioSqlRepository.CreateUsuarioAsync() => [ApiUsuario].[CreateUsuario]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Commands.CreateUsuarioSqlRepository.CreateUsuarioAsync()"); }
    }
}
