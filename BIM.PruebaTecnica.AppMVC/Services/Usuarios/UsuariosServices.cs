using BIM.PruebaTecnica.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BIM.PruebaTecnica.AppMVC.Services.Usuarios;

public class UsuariosServices
    (HttpClient HttpClient)
{
    #region CreateUsuario
    public async Task CreateUsuario(Usuario usuario)
    {
        try
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(usuario),
                Encoding.UTF8,
                "application/json"
            );

            var response = await HttpClient.PostAsync("Api/Usuarios/CreateUsuario", contenido);

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

    #region Login
    public async Task<TokenDto> Login(string user, string password)
    {
        TokenDto result = new TokenDto();
        try
        {
            string userEncoded = Uri.EscapeDataString(user);
            string passEncoded = Uri.EscapeDataString(password);

            var response = await HttpClient.GetAsync($"Api/Usuarios/Login?user={userEncoded}&password={passEncoded}");

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<ProblemDetails>(contentError, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                throw new Exception(problem.Title);
            }

            var content = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<TokenDto>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
        return result;
    }
    #endregion

    #region Encriptar
    public string Encriptar(string texto)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes("ClaveSecreta1234");
        aes.IV = Encoding.UTF8.GetBytes("1234567890123456");

        using var encryptor = aes.CreateEncryptor();
        byte[] buffer = Encoding.UTF8.GetBytes(texto);
        byte[] resultado = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);

        return Convert.ToBase64String(resultado);
    }
    #endregion
}
