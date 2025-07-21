using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Usuarios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Usuarios.Querys;
internal class GetUsuarioByEmailSqlRepository(IOptions<DBOptions> options) : IGetUsuarioByEmailQueryRepository
{
    public async Task<Entities.POCOEntities.Usuarios> GetUsuarioByEmailAsync(string email)
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
                    SqlCommand cmd = new SqlCommand("[ApiUsuario].[GetUsuarioByEmail]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Usuarios>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Querys.GetUsuarioByEmailSqlRepository.GetUsuarioByEmailAsync() => [ApiUsuario].[GetUsuarioByEmail]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Querys.GetUsuarioByEmailSqlRepository.GetUsuarioByEmailAsync()"); }
        return result;
    }
}
