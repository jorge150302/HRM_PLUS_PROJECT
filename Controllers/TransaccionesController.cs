using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM_PLUS_PROJECT.Models;

namespace HRM_PLUS_PROJECT.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly HRMPlusContext _context;

        public TransaccionesController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = _context.Transaccions.Include(t => t.IdEmpleadoNavigation).Include(t => t.IdTipoTransaccionNavigation);
            //return View(await hRMPlusContext.ToListAsync());
            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdTransaccion.ToString().StartsWith(term)
                                            || x.Monto.ToString().Contains(term)
                                            || x.IdEmpleadoNavigation.Nombre.Contains(term)
                                            || x.IdTipoTransaccionNavigation.Nombre.Contains(term)).ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaccions == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.IdTipoTransaccionNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(x => x.IsActivo == true), "IdEmpleado", "FullName");
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccions.Where(x => x.IsActivo == true), "IdTipoTransaccion", "Nombre");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccion,IdEmpleado,IdTipoTransaccion,FechaRegistro,Monto,UsuarioCreacion")] Transaccion transaccion)
        {
            transaccion.UsuarioCreacion = "Marileidy";
            transaccion.FechaRegistro = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(x => x.IsActivo == true), "IdEmpleado",  "FullName");
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccions.Where(x => x.IsActivo == true), "IdTipoTransaccion",  "Nombre");
            return View(transaccion);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaccions == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(x => x.IsActivo == true), "IdEmpleado", "FullName");
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccions.Where(x => x.IsActivo == true), "IdTipoTransaccion", "Nombre");
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccion,IdEmpleado,IdTipoTransaccion,FechaRegistro,Monto,UsuarioCreacion")] Transaccion transaccion)
        {
            if (id != transaccion.IdTransaccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.IdTransaccion))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(x => x.IsActivo == true), "IdEmpleado",  "FullName");
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccions.Where(x => x.IsActivo == true), "IdTipoTransaccion", "Nombre");
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transaccions == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.IdTipoTransaccionNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaccions == null)
            {
                return Problem("Entity set 'HRMPlusContext.Transaccions'  is null.");
            }
            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transaccions.Remove(transaccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
          return (_context.Transaccions?.Any(e => e.IdTransaccion == id)).GetValueOrDefault();
        }
    }
}
