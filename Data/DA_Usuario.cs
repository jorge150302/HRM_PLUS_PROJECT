using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM_PLUS_PROJECT.Models;
using HRM_PLUS_PROJECT.Controllers;

namespace HRM_PLUS_PROJECT.Data
{
    public class DA_Usuario
    {
        //USAR REFERENCIAS MODELS

        public List<Usuario> ListaUsuario()
        {

            return new List<Usuario>
            {
                new Usuario{ Nombre ="jose", Correo = "administrador@gmail.com", Clave= "12345678" , Roles ="Administrador" },
                new Usuario{ Nombre ="juan", Correo = "empleado@gmail.com", Clave= "12345678" , Roles = "Empleado" }

            };

        }

        public Usuario ValidarUsuario(string _correo, string _clave)
        {

            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();

        }

    }

}

