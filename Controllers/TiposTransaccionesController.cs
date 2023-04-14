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
using OfficeOpenXml;

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

        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = from h in _context.TipoTransaccions select h;
            //return View(await hRMPlusContext.ToListAsync());

            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdTipoTransaccion.ToString().StartsWith(term)
                                            || x.Nombre.Contains(term)
                                            || x.Descripcion.Contains(term)
                                            || x.IsActivo.ToString().Contains(term)).ToListAsync());
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
        [Authorize(Roles = "Administrador")]
        // GET: TiposTransacciones/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        // POST: TiposTransacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
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
            catch (Exception)
            {
                TempData["Error"] = "No se pudo eliminar el registro ya que está relacionado a una transacción.";
                return RedirectToAction("Index");
            }
        }

        private bool TipoTransaccionExists(int id)
        {
            return (_context.TipoTransaccions?.Any(e => e.IdTipoTransaccion == id)).GetValueOrDefault();
        }

        //Excel 

        public IActionResult ExportaExcel(string term)
        {
            var query = from tt in _context.TipoTransaccions
                        where string.IsNullOrEmpty(term) || tt.Nombre.Contains(term)
                        select tt;

            var tiposTransacciones = query.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("TiposTransacciones");

                // Agregar encabezados de columna
                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Activo";

                // Agregar datos de fila
                for (int i = 0; i < tiposTransacciones.Count; i++)
                {
                    var tipoTransaccion = tiposTransacciones[i];

                    worksheet.Cells[i + 2, 1].Value = tipoTransaccion.Nombre;
                    worksheet.Cells[i + 2, 2].Value = tipoTransaccion.Descripcion;
                    worksheet.Cells[i + 2, 3].Value = tipoTransaccion.IsActivo;
                }

                // Ajustar ancho de columna
                worksheet.Cells.AutoFitColumns();

                // Devolver archivo Excel como un FileResult
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tiposTransacciones.xlsx");
            }
        }
    }
}
