using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Usuarios.Querys;
internal class GetUsuarioByIdSqlRepository(IOptions<DBOptions> options) : IGetUsuarioByIdQueryRepository
{
    public async Task<Entities.POCOEntities.Usuarios> GetUsuarioByIdAsync(string idUsuario)
    {
        Entities.POCOEntities.Usuarios result = new Entities.POCOEntities.Usuarios();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiUsuario].[GetUsuarioById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = idUsuario;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Usuarios>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Querys.GetUsuarioByIdSqlRepository.GetUsuarioByIdAsync() => [ApiUsuario].[GetUsuarioById]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Querys.GetUsuarioByIdSqlRepository.GetUsuarioByIdAsync()"); }
        return result;
    }
}
