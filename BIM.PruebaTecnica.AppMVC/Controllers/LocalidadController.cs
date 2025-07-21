using BIM.PruebaTecnica.AppMVC.Models;
using BIM.PruebaTecnica.AppMVC.Services.Localidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace BIM.PruebaTecnica.AppMVC.Controllers;
public class LocalidadController(LocalidadServices services) : Controller
{
    #region Crear
    [HttpGet]
    public async Task<IActionResult> Crear()
    {
        try
        {
            LocalidadViewModel modelo = new LocalidadViewModel();

            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            var lstTemp = await services.GetAllEstados(tokenTmp);
            modelo.Estados = lstTemp.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descripcion
            }).ToList();

            return View(modelo);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("Login", "Usuarios");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Crear(LocalidadViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenTmp);
            string sub = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            LocalidadDto result = new LocalidadDto()
            {
                IdEstado = (int)model.IdEstado,
                IdMunicipio = (int)model.IdMunicipio,
                Descripcion = model.Descripcion.Trim(),
                CodigoPostal = model.CodigoPostal,
                IdUsuario = sub
            };
            await services.CrearLocalidad(result, tokenTmp);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        TempData["MensajeExito"] = $"La localidad se guardo correctamente.";
        return RedirectToAction("Crear");
    }
    #endregion

    #region GetMunicipiosByIdEstado
    public async Task<JsonResult> GetMunicipiosByIdEstado(int? estadoId)
    {
        try
        {
            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
            {
                var lista = new List<SelectListItem>
                {
                    new SelectListItem { Text = "-- Seleccione --", Value = "" }
                };
            }

            var lstMunicipios = await services.GetMunicipiosByIdEstado(estadoId, tokenTmp);

            var resultado = lstMunicipios
            .Select(m => new
            {
                id = m.Id,
                municipio = m.Descripcion
            });

            return Json(resultado);
        }
        catch (Exception ex) { throw ex; }
    }
    #endregion

    #region Index
    [HttpGet]
    public async Task<IActionResult> Index(PaginacionDto paginacionDto)
    {
        try
        {
            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenTmp);
            string sub = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            ViewBag.Sub = sub;

            var resultTmp = await services.GetLocalidadByIdUser(sub, tokenTmp, paginacionDto);
            var totalTmp = await services.GetLocalidadTotalPaginas(sub, tokenTmp, paginacionDto);

            var paginacionTmp = new PaginacionDto<LocalidadByIdUserDto>
            {
                LstLocalidadByIdUser = resultTmp,
                PaginaActual = paginacionDto.PaginaActual,
                RegistrosPorPagina = paginacionDto.RegistrosPorPagina,
                TotalPaginas = totalTmp,
                BaseURL = Url.Action()
            };
            return View(paginacionTmp);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("Login", "Usuarios");
        }
    }
    #endregion

    #region Borrar
    public async Task<IActionResult> Borrar(int id)
    {
        try
        {
            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            await services.DeleteLocalidadById(tokenTmp, id);
            return RedirectToAction("Index");
        }
        catch (Exception ex) { throw ex; }
    }
    #endregion

    #region Editar
    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        LocalidadViewModel model = new LocalidadViewModel();
        try
        {
            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            var resultTmp = await services.GetLocalidadById(tokenTmp, id);
            model.Id = resultTmp.Id;
            model.IdEstado = resultTmp.IdEstado;
            model.IdMunicipio = resultTmp.IdMunicipio;
            model.Descripcion = resultTmp.Descripcion;
            model.CodigoPostal = resultTmp.CodigoPostal;

            var lstTemp = await services.GetAllEstados(tokenTmp);
            model.Estados = lstTemp.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descripcion
            }).ToList();

            var lstMunicipios = await services.GetMunicipiosByIdEstado(model.IdEstado, tokenTmp);

            model.Municipios = lstMunicipios.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descripcion
            }).ToList();

            return View("Editar", model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Editar(LocalidadViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var tokenTmp = HttpContext.Session.GetString("Token");
            if (tokenTmp == null)
                return RedirectToAction("Login", "Usuarios");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenTmp);
            string sub = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            UpdateLocalidadDto result = new UpdateLocalidadDto()
            {
                Id = (int)model.Id,
                IdMunicipio = (int)model.IdMunicipio,
                Descripcion = model.Descripcion.Trim(),
                CodigoPostal = model.CodigoPostal,
                IdUsuario = sub
            };
            await services.ActualizarLocalidad(result, tokenTmp);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        TempData["MensajeExito"] = $"Cambios guardados correctamente.";
        return RedirectToAction("Editar");
    }
    #endregion
}
