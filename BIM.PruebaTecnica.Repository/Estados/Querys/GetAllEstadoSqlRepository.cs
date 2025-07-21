using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Estados.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Estados.Querys;
internal class GetAllEstadoSqlRepository(IOptions<DBOptions> options) : IGetAllEstadoQueryRepository
{
    public async Task<IEnumerable<Entities.POCOEntities.Estados>> GetAllEstadosAsync()
    {
        List<Entities.POCOEntities.Estados> lstResult = new List<Entities.POCOEntities.Estados>();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiEstados].[GetAllEstado]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        lstResult = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Estados>>(JsonConvert.SerializeObject(dt));
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Estados.Querys.GetAllEstadoSqlRepository/GetAllEstadosAsync() => [ApiEstados].[GetAllEstado]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Estados.Querys.GetAllEstadoSqlRepository/GetAllEstadosAsync()"); }
        return lstResult;
    }
}
