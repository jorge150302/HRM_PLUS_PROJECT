﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM_PLUS_PROJECT.Models;

namespace HRM_PLUS_PROJECT.Controllers
{
    public class TiposTransaccionesController : Controller
    {
        private readonly HRMPlusContext _context;

        public TiposTransaccionesController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: TiposTransacciones
        public async Task<IActionResult> Index()
        {
              return _context.TipoTransaccions != null ? 
                          View(await _context.TipoTransaccions.ToListAsync()) :
                          Problem("Entity set 'HRMPlusContext.TipoTransaccions'  is null.");
        }

        // GET: TiposTransacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoTransaccions == null)
            {
                return NotFound();
            }

            var tipoTransaccion = await _context.TipoTransaccions
                .FirstOrDefaultAsync(m => m.IdTipoTransaccion == id);
            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            return View(tipoTransaccion);
        }

        // GET: TiposTransacciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposTransacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoTransaccion,Nombre,Descripcion,IsActivo")] TipoTransaccion tipoTransaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoTransaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTransaccion);
        }

        // GET: TiposTransacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoTransaccions == null)
            {
                return NotFound();
            }

            var tipoTransaccion = await _context.TipoTransaccions.FindAsync(id);
            if (tipoTransaccion == null)
            {
                return NotFound();
            }
            return View(tipoTransaccion);
        }

        // POST: TiposTransacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoTransaccion,Nombre,Descripcion,IsActivo")] TipoTransaccion tipoTransaccion)
        {
            if (id != tipoTransaccion.IdTipoTransaccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoTransaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTransaccionExists(tipoTransaccion.IdTipoTransaccion))
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
            return View(tipoTransaccion);
        }

        // GET: TiposTransacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoTransaccions == null)
            {
                return NotFound();
            }

            var tipoTransaccion = await _context.TipoTransaccions
                .FirstOrDefaultAsync(m => m.IdTipoTransaccion == id);
            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            return View(tipoTransaccion);
        }

        // POST: TiposTransacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoTransaccions == null)
            {
                return Problem("Entity set 'HRMPlusContext.TipoTransaccions'  is null.");
            }
            var tipoTransaccion = await _context.TipoTransaccions.FindAsync(id);
            if (tipoTransaccion != null)
            {
                _context.TipoTransaccions.Remove(tipoTransaccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTransaccionExists(int id)
        {
          return (_context.TipoTransaccions?.Any(e => e.IdTipoTransaccion == id)).GetValueOrDefault();
        }
    }
}
