using Microsoft.AspNetCore.Mvc;


using HRM_PLUS_PROJECT.Models;
using HRM_PLUS_PROJECT.Data;

//1.- REFERENCES AUTHENTICATION COOKIE
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HRM_PLUS_PROJECT.Controllers
{
    public class AccesoController : Controller
    {
        
                public IActionResult Index()
                {
                    return View();
                }

                //USAR REFERENCIAS Models y Data
                [HttpPost]
                public async Task<IActionResult> Index(Usuario _usuario)
                {
                    DA_Usuario _da_usuario = new DA_Usuario();

                    var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);

                    if (usuario != null)
                    {

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.Nombre),
                            new Claim("Correo", usuario.Correo),
                            new Claim(ClaimTypes.Role, usuario.Roles)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Usuario y/o contraseña no coinciden.";
                        return View();
                    }

                }

                public async Task<IActionResult> Salir()
                {
                    //3.- CONFIGURACION DE LA AUTENTICACION
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


                    return RedirectToAction("Index");

                }

            }
        
    }

