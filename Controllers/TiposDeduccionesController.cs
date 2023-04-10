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

        //Excel 

        public ActionResult ExportaExcel()
        {
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Tipos de Deducciones");
                worksheet.Cells["A1"].Value = "Nombre";
                worksheet.Cells["B1"].Value = "Descripción";
                worksheet.Cells["C1"].Value = "Monto";
                worksheet.Cells["D1"].Value = "Todo el empleado";
                worksheet.Cells["E1"].Value = "Mínimo rango";
                worksheet.Cells["F1"].Value = "Máximo rango";
                worksheet.Cells["G1"].Value = "Activo";

                var tiposDeducciones = _context.TipoDeduccions.ToList();
                int row = 2;

                foreach (var tipoDeduccion in tiposDeducciones)
                {
                    worksheet.Cells[string.Format("A{0}", row)].Value = tipoDeduccion.Nombre;
                    worksheet.Cells[string.Format("B{0}", row)].Value = tipoDeduccion.Descripcion;
                    worksheet.Cells[string.Format("C{0}", row)].Value = tipoDeduccion.Monto;
                    worksheet.Cells[string.Format("D{0}", row)].Value = tipoDeduccion.IsTodoEmpleado;
                    worksheet.Cells[string.Format("E{0}", row)].Value = tipoDeduccion.MinimoRango;
                    worksheet.Cells[string.Format("F{0}", row)].Value = tipoDeduccion.MaximoRango;
                    worksheet.Cells[string.Format("G{0}", row)].Value = tipoDeduccion.IsActivo;
                    row++;
                }

                fileContents = package.GetAsByteArray();
            }

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TiposDeducciones.xlsx");
        }
    }
}
