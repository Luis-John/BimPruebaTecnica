using BIM.PruebaTecnica.AppMVC.Models;
using BIM.PruebaTecnica.AppMVC.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace BIM.PruebaTecnica.AppMVC.Controllers;
public class UsuariosController(UsuariosServices usuariosClient) : Controller
{

    #region Registro
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel model)
    {
        try
        {
            Usuario usuario = new Usuario();

            if (!ModelState.IsValid)
                return View(model);

            usuario.NombreUsuario = model.NombreUsuario.Trim();
            usuario.Password = usuariosClient.Encriptar(model.Password.Trim());
            usuario.Email = model.Email.Trim();

            await usuariosClient.CreateUsuario(usuario);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        return RedirectToAction("Login");
    }
    #endregion

    #region Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }
    #endregion

    #region Login
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel modelo)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var passwordTmp = usuariosClient.Encriptar(modelo.Password.Trim());

            var resultToken = await usuariosClient.Login(modelo.NombreUsuario, passwordTmp);
            HttpContext.Session.SetString("Token", resultToken.Token);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(modelo);
        }
        return RedirectToAction("Index", "Localidad");
    }
    #endregion

}
