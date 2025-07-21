using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Commands;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Localidad.Commands;
internal class CreateLocalidadSqlRepository(IOptions<DBOptions> options) : ICreateLocalidadCommandRepository
{
    public async Task CreateLocalidadAsync(Entities.POCOEntities.Localidad localidad)
    {
        try
        {
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(options.Value.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[ApiLocalidad].[CreateLocalidad]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Localidad", SqlDbType.VarChar).Value = localidad.Descripcion;
                    cmd.Parameters.Add("@CodigoPostal", SqlDbType.Int).Value = localidad.CodigoPostal;
                    cmd.Parameters.Add("@IdMunicipio", SqlDbType.Int).Value = localidad.IdMunicipio;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.VarChar).Value = localidad.IdUsuario;
                    cmd.CommandTimeout = 120;
                    int r = cmd.ExecuteNonQuery();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Commands.CreateLocalidadSqlRepository.CreateLocalidadAsync() => [ApiLocalidad].[CreateLocalidad]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Commands.CreateLocalidadSqlRepository.CreateLocalidadAsync()"); }
    }
}
