using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Login;
using BIM.PruebaTecnica.Entities.Interfaces.Usuarios;


using Microsoft.AspNetCore.Mvc;

namespace BIM.PruebaTecnica.WebApi.Controllers;
[ApiController]
[Route("Api/Usuarios")]
public class UsuariosController(
    ICreateUsuarioInputPort createInputPort,
    IGetLoginInputPort LoginInputPort,
    IGetLoginOutputPort LoginOutputPort) : ControllerBase
{

    #region CreateUsuario
    [HttpPost]
    [Route("CreateUsuario")]
    public async Task<IActionResult> CreateUsuario([FromBody] UsuariosDto usuario)
    {
        try
        {
            await createInputPort.CreateUsuarioAsyn(usuario);
            return Created();
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region Login
    [HttpGet]
    [Route("Login")]
    public async Task<IActionResult> Login(string user, string password)
    {
        try
        {
            await LoginInputPort.LoginAsync(user, password);
            return Ok(LoginOutputPort.Token);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion
}
