﻿using System;
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
    public class TiposDeduccionesController : Controller
    {
        private readonly HRMPlusContext _context;

        public TiposDeduccionesController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: TiposDeducciones

        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = from h in _context.TipoDeduccions select h;
            //return View(await hRMPlusContext.ToListAsync());

            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdDeduccion.ToString().StartsWith(term)
                                            || x.Nombre.Contains(term)
                                            || x.Descripcion.Contains(term)
                                            || x.Monto.ToString().Contains(term)
                                            || x.IsTodoEmpleado.ToString().Contains(term)
                                            || x.MinimoRango.ToString().Contains(term)
                                            || x.MaximoRango.ToString().Contains(term)
                                            || x.IsActivo.ToString().Contains(term)).ToListAsync());
        }

        // GET: TiposDeducciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoDeduccions == null)
            {
                return NotFound();
            }

            var tipoDeduccion = await _context.TipoDeduccions
                .FirstOrDefaultAsync(m => m.IdDeduccion == id);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            return View(tipoDeduccion);
        }
        [Authorize(Roles = "Administrador")]
        // GET: TiposDeducciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposDeducciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDeduccion,Nombre,Descripcion,Monto,IsTodoEmpleado,MinimoRango,MaximoRango,IsActivo")] TipoDeduccion tipoDeduccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDeduccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDeduccion);
        }
        [Authorize(Roles = "Administrador")]
        // GET: TiposDeducciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoDeduccions == null)
            {
                return NotFound();
            }

            var tipoDeduccion = await _context.TipoDeduccions.FindAsync(id);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }
            return View(tipoDeduccion);
        }
        [Authorize(Roles = "Administrador")]
        // POST: TiposDeducciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDeduccion,Nombre,Descripcion,Monto,IsTodoEmpleado,MinimoRango,MaximoRango,IsActivo")] TipoDeduccion tipoDeduccion)
        {
            if (id != tipoDeduccion.IdDeduccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDeduccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDeduccionExists(tipoDeduccion.IdDeduccion))
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
            return View(tipoDeduccion);
        }
        [Authorize(Roles = "Administrador")]
        // GET: TiposDeducciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoDeduccions == null)
            {
                return NotFound();
            }

            var tipoDeduccion = await _context.TipoDeduccions
                .FirstOrDefaultAsync(m => m.IdDeduccion == id);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            return View(tipoDeduccion);
        }
        [Authorize(Roles = "Administrador")]
        // POST: TiposDeducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoDeduccions == null)
            {
                return Problem("Entity set 'HRMPlusContext.TipoDeduccions'  is null.");
            }
            var tipoDeduccion = await _context.TipoDeduccions.FindAsync(id);
            if (tipoDeduccion != null)
            {
                _context.TipoDeduccions.Remove(tipoDeduccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDeduccionExists(int id)
        {
          return (_context.TipoDeduccions?.Any(e => e.IdDeduccion == id)).GetValueOrDefault();
        }
    }
}
