using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Localidad.Querys;
internal class GetLocalidadByIdSqlRepository(IOptions<DBOptions> options) : IGetLocalidadByIdQueryRepository
{
    public async Task<LocalidadByIdDto> GetLocalidadByIdAsync(int id)
    {
        LocalidadByIdDto result = new LocalidadByIdDto();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiLocalidad].[GetLocalidadById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = JsonConvert.DeserializeObject<List<LocalidadByIdDto>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Querys.GetLocalidadByIdSqlRepository.GetLocalidadByIdAsync() => [ApiLocalidad].[GetLocalidadById]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Querys.GetLocalidadByIdSqlRepository.GetLocalidadByIdAsync()"); }
        return result;
    }
}
