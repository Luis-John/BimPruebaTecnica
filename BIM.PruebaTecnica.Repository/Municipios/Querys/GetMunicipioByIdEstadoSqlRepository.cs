using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Municipios.Querys;
internal class GetMunicipioByIdEstadoSqlRepository(IOptions<DBOptions> options) : IGetMunicipioByIdEstadoQueryRepository
{
    public async Task<IEnumerable<Entities.POCOEntities.Municipios>> GetMunicipioByIdEstadoAsync(int idEstado)
    {
        List<Entities.POCOEntities.Municipios> result = new List<Entities.POCOEntities.Municipios>();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiMunicipio].[GetMunicipioByIdEstado]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = idEstado;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Municipios>>(JsonConvert.SerializeObject(dt));
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Municipios.Querys.GetMunicipioByIdEstadoSqlRepository.GetMunicipiosByIdEstadoAsync() => [ApiMunicipio].[GetMunicipioByIdEstado]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Municipios.Querys.GetMunicipioByIdEstadoSqlRepository.GetMunicipiosByIdEstadoAsync"); }
        return result;
    }
}
