using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Estados.Querys;
internal class GetEstadoByIdSqlRepository(IOptions<DBOptions> options) : IGetEstadoByIdQueryRepository
{
    public async Task<Entities.POCOEntities.Estados> GetEstadoByIdAsync(int id)
    {
        Entities.POCOEntities.Estados result = new Entities.POCOEntities.Estados();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiEstados].[GetEstadoById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Estados>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Querys.GetEstadoByIdSqlRepository.GetEstadoByIdAsync() => [ApiEstados].[GetEstadoById]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Querys.GetEstadoByIdSqlRepository.GetEstadoByIdAsync()"); }
        return result;
    }
}
