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
    public class NominasController : Controller
    {
        private readonly HRMPlusContext _context;

        public NominasController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: Nominas
        public async Task<IActionResult> Index()
        {
            var hRMPlusContext = _context.Nominas.Include(n => n.IdDeduccionNavigation).Include(n => n.IdEmpleadoNavigation).Include(n => n.IdTransaccionNavigation);
            return View(await hRMPlusContext.ToListAsync());
        }

        // GET: Nominas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas
                .Include(n => n.IdDeduccionNavigation)
                .Include(n => n.IdEmpleadoNavigation)
                .Include(n => n.IdTransaccionNavigation)
                .FirstOrDefaultAsync(m => m.IdNomina == id);
            if (nomina == null)
            {
                return NotFound();
            }

            return View(nomina);
        }

        // GET: Nominas/Create
        public IActionResult Create()
        {
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "IdDeduccion");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdTransaccion"] = new SelectList(_context.Transaccions, "IdTransaccion", "IdTransaccion");
            return View();
        }

        // POST: Nominas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNomina,IdEmpleado,IdDeduccion,IdTransaccion,Monto")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nomina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "IdDeduccion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", nomina.IdEmpleado);
            ViewData["IdTransaccion"] = new SelectList(_context.Transaccions, "IdTransaccion", "IdTransaccion", nomina.IdTransaccion);
            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina == null)
            {
                return NotFound();
            }
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "IdDeduccion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", nomina.IdEmpleado);
            ViewData["IdTransaccion"] = new SelectList(_context.Transaccions, "IdTransaccion", "IdTransaccion", nomina.IdTransaccion);
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNomina,IdEmpleado,IdDeduccion,IdTransaccion,Monto")] Nomina nomina)
        {
            if (id != nomina.IdNomina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nomina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NominaExists(nomina.IdNomina))
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
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "IdDeduccion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", nomina.IdEmpleado);
            ViewData["IdTransaccion"] = new SelectList(_context.Transaccions, "IdTransaccion", "IdTransaccion", nomina.IdTransaccion);
            return View(nomina);
        }

        // GET: Nominas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas
                .Include(n => n.IdDeduccionNavigation)
                .Include(n => n.IdEmpleadoNavigation)
                .Include(n => n.IdTransaccionNavigation)
                .FirstOrDefaultAsync(m => m.IdNomina == id);
            if (nomina == null)
            {
                return NotFound();
            }

            return View(nomina);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nominas == null)
            {
                return Problem("Entity set 'HRMPlusContext.Nominas'  is null.");
            }
            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina != null)
            {
                _context.Nominas.Remove(nomina);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NominaExists(int id)
        {
          return (_context.Nominas?.Any(e => e.IdNomina == id)).GetValueOrDefault();
        }
    }
}
