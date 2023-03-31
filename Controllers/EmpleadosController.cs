using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM_PLUS_PROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HRM_PLUS_PROJECT.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor,Empleado,Jefe")]
    public class EmpleadosController : Controller
    {
        private readonly HRMPlusContext _context;

        public EmpleadosController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = _context.Empleados.Include(e => e.IdDepartamentoNavigation).Include(e => e.IdPuestoNavigation);
            //return View(await hRMPlusContext.ToListAsync());

            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdEmpleado.ToString().StartsWith(term)
                                            || x.Cedula.Contains(term)
                                            || x.Apellido.Contains(term)
                                            || x.Nombre.Contains(term)
                                            || x.SalarioMensual.ToString().Contains(term)
                                            || x.IdDepartamentoNavigation.Nombre.Contains(term)
                                            || x.IdPuestoNavigation.Nombre.Contains(term)
                                            || x.IsActivo.ToString().Contains(term)).ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos.Where(x => x.IsActivo == true), "IdDepartamento", "Nombre");
            ViewData["IdPuesto"] = new SelectList(_context.Puestos.Where(x => x.IsActivo == true), "IdPuesto", "Nombre");
            return View();
        }
        [Authorize(Roles = "Administrador")]
        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,Cedula,Nombre,Apellido,Telefono,IdDepartamento,IdPuesto,SalarioMensual,FechaRegistro,UsuarioCreacion,IsActivo")] Empleado empleado)
        {
            if (!validaCedula(empleado.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cédula incorrecta");
            }

            Puesto puestos = _context.Puestos.Find(empleado.IdPuesto);

            if (empleado.SalarioMensual<puestos.SalarioMinimo) {
                ModelState.AddModelError("SalarioMensual", "El Salario es menor al mínimo " + puestos.SalarioMinimo);
            }

            if (empleado.SalarioMensual>puestos.SalarioMaximo)
            {
                ModelState.AddModelError("SalarioMensual", "El Salario es mayor al máximo " + puestos.SalarioMaximo);
            }


            empleado.UsuarioCreacion = "Marileidy";
            empleado.FechaRegistro = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento", "Nombre");
            ViewData["IdPuesto"] = new SelectList(_context.Puestos, "IdPuesto", "IdPuesto", "Nombre");
            return View(empleado);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos.Where(x => x.IsActivo == true), "IdDepartamento", "Nombre");
            ViewData["IdPuesto"] = new SelectList(_context.Puestos.Where(x => x.IsActivo == true), "IdPuesto", "Nombre");
            return View(empleado);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,Cedula,Nombre,Apellido,Telefono,IdDepartamento,IdPuesto,SalarioMensual,FechaRegistro,UsuarioCreacion,IsActivo")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }

            if (!validaCedula(empleado.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cédula incorrecta");
            }

            Puesto puestos = _context.Puestos.Find(empleado.IdPuesto);

            if (empleado.SalarioMensual < puestos.SalarioMinimo)
            {
                ModelState.AddModelError("SalarioMensual", "El Salario es menor al mínimo " + puestos.SalarioMinimo);
            }

            if (empleado.SalarioMensual > puestos.SalarioMaximo)
            {
                ModelState.AddModelError("SalarioMensual", "El Salario es mayor al máximo " + puestos.SalarioMaximo);
            }


            empleado.UsuarioCreacion = "Marileidy";

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento", "Nombre");
            ViewData["IdPuesto"] = new SelectList(_context.Puestos, "IdPuesto", "IdPuesto", "Nombre");
            return View(empleado);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'HRMPlusContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        }

        public static bool validaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }

    }
}
