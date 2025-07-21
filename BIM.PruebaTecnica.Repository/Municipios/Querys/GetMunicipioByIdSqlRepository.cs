using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Municipios.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Municipios.Querys;
internal class GetMunicipioByIdSqlRepository(IOptions<DBOptions> options) : IGetMunicipioByIdQueryRepository
{
    public async Task<Entities.POCOEntities.Municipios> GetMunicipioAsync(int id)
    {
        Entities.POCOEntities.Municipios result = new Entities.POCOEntities.Municipios();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiMunicipio].[GetMunicipioById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<Entities.POCOEntities.Municipios>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Querys.GetMunicipioByIdSqlRepository.GetMunicipioAsync() => [ApiMunicipio].[GetMunicipioById]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Querys.GetMunicipioByIdSqlRepository.GetMunicipioAsync()"); }
        return result;
    }
}
