using BIM.PruebaTecnica.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BIM.PruebaTecnica.AppMVC.Services.Localidad;

public class LocalidadServices(HttpClient HttpClient)
{

    #region CrearLocalidad
    public async Task CrearLocalidad(LocalidadDto localidad, string token)
    {
        try
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(localidad),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.PostAsync("Api/Localidad/CreateLocalidad", contenido);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }
        }
        catch (Exception ex) { throw ex; }
    }
    #endregion

    #region DeleteLocalidadById
    public async Task<LocalidadByIdUserDto> DeleteLocalidadById(string token, int id)
    {
        LocalidadByIdUserDto result = new LocalidadByIdUserDto();
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.DeleteAsync($"Api/Localidad/DeleteLocalidad?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

        }
        catch (Exception ex) { throw ex; }
        return result;
    }
    #endregion

    #region GetLocalidadById
    public async Task<LocalidadByIdUserDto> GetLocalidadById(string token, int id)
    {
        LocalidadByIdUserDto result = new LocalidadByIdUserDto();
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.GetAsync($"Api/Localidad/GetLocalidadById?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var content = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<LocalidadByIdUserDto>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
        return result;
    }
    #endregion

    #region ActualizarLocalidad
    public async Task ActualizarLocalidad(UpdateLocalidadDto localidad, string token)
    {
        try
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(localidad),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.PutAsync("Api/Localidad/UpdateLocalidad", contenido);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }
        }
        catch (Exception ex) { throw ex; }
    }
    #endregion

    #region GetAllEstados
    public async Task<List<EstadosDto>> GetAllEstados(string token)
    {
        List<EstadosDto> lstResult = new List<EstadosDto>();
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.GetAsync($"Api/Localidad/GetAllEstados");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var content = await response.Content.ReadAsStringAsync();
            lstResult = JsonSerializer.Deserialize<List<EstadosDto>>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
        return lstResult;
    }
    #endregion

    #region GetMunicipiosByIdEstado
    public async Task<List<MunicipiosDto>> GetMunicipiosByIdEstado(int? idEstado, string token)
    {
        List<MunicipiosDto> lstResult = new List<MunicipiosDto>();
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.GetAsync($"Api/Localidad/GetMunicipiosByIdEstado?IdEstado={idEstado}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var content = await response.Content.ReadAsStringAsync();
            lstResult = JsonSerializer.Deserialize<List<MunicipiosDto>>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
        return lstResult;
    }
    #endregion

    #region GetLocalidadByIdUser
    public async Task<List<LocalidadByIdUserDto>> GetLocalidadByIdUser(string idUsuario, string token, PaginacionDto paginacionDto)
    {
        List<LocalidadByIdUserDto> lstResult = new List<LocalidadByIdUserDto>();
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.GetAsync($"Api/Localidad/GetLocalidadByIdUser?idUsuario={idUsuario}&pagina={paginacionDto.PaginaActual}&registroPorPagina={paginacionDto.RegistrosPorPagina}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var content = await response.Content.ReadAsStringAsync();
            lstResult = JsonSerializer.Deserialize<List<LocalidadByIdUserDto>>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
        return lstResult;
    }
    #endregion

    #region GetLocalidadTotalPaginas
    public async Task<int> GetLocalidadTotalPaginas(string idUsuario, string token, PaginacionDto paginacionDto)
    {
        int result = default;
        try
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await HttpClient.GetAsync($"Api/Localidad/GetLocalidadTotalPaginas?idUsuario={idUsuario}&registrosPorPagina={paginacionDto.RegistrosPorPagina}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var contenido = await response.Content.ReadAsStringAsync();
            if (int.TryParse(contenido, out int r))
                result = r;
        }
        catch (Exception ex) { throw ex; }
        return result;
    }
    #endregion


}
