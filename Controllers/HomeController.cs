using System.Data;
using System.Diagnostics;
using HRM_PLUS_PROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HRM_PLUS_PROJECT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador,Supervisor,Empleado,Jefe")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Empleado")]
        public IActionResult Nominas()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult Puestos()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult TiposDeduccion()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult TiposTransaccion()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult Transsaccion()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
