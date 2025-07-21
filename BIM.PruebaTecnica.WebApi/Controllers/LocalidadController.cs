using BIM.PruebaTecnica.Entities.Dtos;
using BIM.PruebaTecnica.Entities.Exceptions;
using BIM.PruebaTecnica.Entities.Interfaces.Estados;
using BIM.PruebaTecnica.Entities.Interfaces.Localidad;
using BIM.PruebaTecnica.Entities.Interfaces.Municipios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BIM.PruebaTecnica.WebApi.Controllers;

[Authorize]
[Route("Api/Localidad")]
[ApiController]
public class LocalidadController(
    ICreateLocalidadInputPort inputPort,
    IGetAllEstadosInputPort GetAllEstadosInputPort,
    IGetMunicipioByIdEstadoInputPort GetMunicipioByIdEstadoInputPort,
    IGetLocalidadByIdUserInputPort GetLocalidadByIdUserInputPort,
    IGetLocalidadTotalPaginasInputPort GetLocalidadTotalPaginasInputPort,
    IGetLocalidadByIdInputPort GetLocalidadByIdInputPort,
    IUpdateLocalidadInputPort UpdateLocalidadInputPort,
    IDeleteLocalidadInputPort DeleteLocalidadInputPort
    ) : ControllerBase
{

    #region DeleteLocalidad
    [HttpDelete]
    [Route("DeleteLocalidad")]
    public async Task<IActionResult> DeleteLocalidad(int id)
    {
        try
        {
            await DeleteLocalidadInputPort.DeleteLocalidadAsync(id);
            return NoContent();
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region GetAllEstados
    [HttpGet]
    [Route("GetAllEstados")]
    public async Task<IActionResult> GetAllEstados()
    {
        try
        {
            var lstResult = await GetAllEstadosInputPort.GetAllEstadosAsync();
            return Ok(lstResult);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region GetMunicipiosByIdEstado
    [HttpGet]
    [Route("GetMunicipiosByIdEstado")]
    public async Task<IActionResult> GetMunicipiosByIdEstado(int idEstado)
    {
        try
        {
            var lstResult = await GetMunicipioByIdEstadoInputPort.GetMunicipiosByIdEstadoAsync(idEstado);
            return Ok(lstResult);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }

    #endregion

    #region CreateLocalidad
    [HttpPost]
    [Route("CreateLocalidad")]
    public async Task<IActionResult> CreateLocalidad([FromBody] LocalidadDto localidad)
    {
        try
        {
            await inputPort.CreateLocalidadAsync(localidad);
            return Created();
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region GetLocalidadByIdUser
    [HttpGet]
    [Route("GetLocalidadByIdUser")]
    public async Task<IActionResult> GetLocalidadByIdUser(string idUsuario, int pagina, int registroPorPagina)
    {
        try
        {
            var lstResult = await GetLocalidadByIdUserInputPort.GetLocalidadByIdUserAsync(idUsuario, pagina, registroPorPagina);
            return Ok(lstResult);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region GetLocalidadTotalPaginas
    [HttpGet]
    [Route("GetLocalidadTotalPaginas")]
    public async Task<IActionResult> GetLocalidadTotalPaginas(string idUsuario, int registrosPorPagina)
    {
        try
        {
            var lstResult = await GetLocalidadTotalPaginasInputPort.GetLocalidadTotalPaginasAsync(idUsuario, registrosPorPagina);
            return Ok(lstResult);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region GetLocalidadById
    [HttpGet]
    [Route("GetLocalidadById")]
    public async Task<IActionResult> GetLocalidadById(int id)
    {
        try
        {
            var result = await GetLocalidadByIdInputPort.GetLocalidadByIdAsync(id);
            return Ok(result);
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion

    #region UpdateLocalidad
    [HttpPut]
    [Route("UpdateLocalidad")]
    public async Task<IActionResult> UpdateLocalidad(UpdateLocalidadDto localidad)
    {
        try
        {
            await UpdateLocalidadInputPort.UpdateLocalidadAsync(localidad);
            return NoContent();
        }
        catch (UnauthorizationException ue) { return Problem(ue.Detalle, HttpContext.Request.Path, ue.StatusCode, ue.Mensaje); }
        catch (BadRequestException bre) { return Problem(bre.Detalle, HttpContext.Request.Path, bre.StatusCode, bre.Mensaje); }
        catch (InternalApiException iae) { return Problem(iae.Detalle, HttpContext.Request.Path, iae.StatusCode, iae.Mensaje); }
        catch (Exception ex) { return Problem(ex.Message, HttpContext.Request.Path, 500, "Error interno no contraldo, favor de volver a intentar"); }
    }
    #endregion
}
