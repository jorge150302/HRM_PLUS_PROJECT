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

    public class PuestosController : Controller
    {
        private readonly HRMPlusContext _context;

        public PuestosController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: Puestos
        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = from h in _context.Puestos select h;
            //return View(await hRMPlusContext.ToListAsync());

            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdPuesto.ToString().StartsWith(term)
                                            || x.Nombre.Contains(term)
                                            || x.Descripcion.Contains(term)
                                            || x.NivelRiesgo.Contains(term)
                                            || x.SalarioMinimo.ToString().Contains(term)
                                            || x.SalarioMaximo.ToString().Contains(term)
                                            || x.IsActivo.ToString().Contains(term)).ToListAsync());
        }

        // GET: Puestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.Puestos
                .FirstOrDefaultAsync(m => m.IdPuesto == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        // POST: Puestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPuesto,Nombre,Descripcion,NivelRiesgo,SalarioMinimo,SalarioMaximo,IsActivo")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puesto);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.Puestos.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }
            return View(puesto);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Puestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPuesto,Nombre,Descripcion,NivelRiesgo,SalarioMinimo,SalarioMaximo,IsActivo")] Puesto puesto)
        {
            if (id != puesto.IdPuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoExists(puesto.IdPuesto))
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
            return View(puesto);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.Puestos
                .FirstOrDefaultAsync(m => m.IdPuesto == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Puestos == null)
                {
                    return Problem("Entity set 'HRMPlusContext.Puestos'  is null.");
                }
                var puesto = await _context.Puestos.FindAsync(id);
                if (puesto != null)
                {
                    _context.Puestos.Remove(puesto);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "No se pudo eliminar el registro ya que está relacionado a un empleado.";
                return RedirectToAction("Index");
            }
        }

        private bool PuestoExists(int id)
        {
            return (_context.Puestos?.Any(e => e.IdPuesto == id)).GetValueOrDefault();
        }

        //Excel

        public IActionResult ExportaExcel(string term)
        {
            var query = from p in _context.Puestos
                        where string.IsNullOrEmpty(term) || p.Nombre.Contains(term)
                        select p;

            var puestos = query.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Puestos");

                // Agregar encabezados de columna
                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Nivel de Riesgo";
                worksheet.Cells[1, 4].Value = "Salario Mínimo";
                worksheet.Cells[1, 5].Value = "Salario Máximo";
                worksheet.Cells[1, 6].Value = "Activo";

                // Agregar datos de fila
                for (int i = 0; i < puestos.Count; i++)
                {
                    var puesto = puestos[i];

                    worksheet.Cells[i + 2, 1].Value = puesto.Nombre;
                    worksheet.Cells[i + 2, 2].Value = puesto.Descripcion;
                    worksheet.Cells[i + 2, 3].Value = puesto.NivelRiesgo;
                    worksheet.Cells[i + 2, 4].Value = puesto.SalarioMinimo;
                    worksheet.Cells[i + 2, 5].Value = puesto.SalarioMaximo;
                    worksheet.Cells[i + 2, 6].Value = puesto.IsActivo;
                }

                // Ajustar ancho de columna
                worksheet.Cells.AutoFitColumns();

                // Devolver archivo Excel como un FileResult
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "puestos.xlsx");
            }
        }
    }
}
