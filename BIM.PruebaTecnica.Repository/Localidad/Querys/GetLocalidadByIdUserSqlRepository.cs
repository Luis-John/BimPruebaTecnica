using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Repositories.Localidad.Querys;
using BIM.PruebaTecnica.Entities.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace BIM.PruebaTecnica.Repository.Localidad.Querys;
internal class GetLocalidadByIdUserSqlRepository(IOptions<DBOptions> options) : IGetLocalidadByIdUserQueryRepository
{
    public async Task<IEnumerable<LocalidadByIdUserDto>> GetLocalidadByIdUserAsync(string idUsuario, int pagina, int registroPorPagina)
    {
        List<LocalidadByIdUserDto> lstResult = new List<LocalidadByIdUserDto>();
        try
        {
            await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(options.Value.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[ApiLocalidad].[GetLocalidadByIdUser]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaginaActual", SqlDbType.Int).Value = pagina;
                    cmd.Parameters.Add("@RegistrosPorPagina", SqlDbType.Int).Value = registroPorPagina;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.VarChar).Value = idUsuario;
                    cmd.CommandTimeout = 120;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        lstResult = JsonConvert.DeserializeObject<List<LocalidadByIdUserDto>>(JsonConvert.SerializeObject(dt));
                }
            });
        }
        catch (SqlException se) { throw new InternalApiException("Error acceso a datos", se.Message, "BIM.PruebaTecnica.Repository.Localidad.Querys.GetLocalidadByIdUserSqlRepository.GetLocalidadByIdUserAsync() => [ApiLocalidad].[GetLocalidadByIdUser]"); }
        catch (Exception ex) { throw new InternalApiException("Error acceso a datos", ex.Message, "BIM.PruebaTecnica.Repository.Localidad.Querys.GetLocalidadByIdUserSqlRepository.GetLocalidadByIdUserAsync()"); }
        return lstResult;
    }
}
