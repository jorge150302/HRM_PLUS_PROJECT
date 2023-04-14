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

    public class DepartamentosController : Controller
    {
        private readonly HRMPlusContext _context;

        public DepartamentosController(HRMPlusContext context)
        {
            _context = context;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = from h in _context.Departamentos select h;
            //return View(await hRMPlusContext.ToListAsync());

            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdDepartamento.ToString().StartsWith(term)
                                            || x.Nombre.Contains(term)
                                            || x.Descripcion.Contains(term)
                                            || x.UbicacionFisica.Contains(term)
                                            || x.IsActivo.ToString().Contains(term)).ToListAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.IdDepartamento == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDepartamento,Nombre,Descripcion,UbicacionFisica,IsActivo")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDepartamento,Nombre,Descripcion,UbicacionFisica,IsActivo")] Departamento departamento)
        {
            if (id != departamento.IdDepartamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.IdDepartamento))
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
            return View(departamento);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.IdDepartamento == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Departamentos == null)
                {
                    return Problem("Entity set 'HRMPlusContext.Departamentos'  is null.");
                }
                var departamento = await _context.Departamentos.FindAsync(id);
                if (departamento != null)
                {
                    _context.Departamentos.Remove(departamento);
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


        private bool DepartamentoExists(int id)
        {
            return (_context.Departamentos?.Any(e => e.IdDepartamento == id)).GetValueOrDefault();
        }

        //Excel

        public IActionResult ExportaExcel(string term)
        {
            // Obtener los datos de los departamentos
            var departamentos = _context.Departamentos.ToList();

            if (!string.IsNullOrEmpty(term))
            {
                departamentos = departamentos.Where(d => d.Nombre.Contains(term) || d.Descripcion.Contains(term)).ToList();
            }

            // Crear el archivo de Excel
            using (var package = new ExcelPackage())
            {
                // Agregar una hoja al archivo
                var worksheet = package.Workbook.Worksheets.Add("Departamentos");

                // Escribir los encabezados de las columnas
                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Ubicación Física";
                worksheet.Cells[1, 4].Value = "Activo";

                // Escribir los datos de los departamentos en las filas
                int row = 2;
                foreach (var departamento in departamentos)
                {
                    worksheet.Cells[row, 1].Value = departamento.Nombre;
                    worksheet.Cells[row, 2].Value = departamento.Descripcion;
                    worksheet.Cells[row, 3].Value = departamento.UbicacionFisica;
                    worksheet.Cells[row, 4].Value = departamento.IsActivo ? "Sí" : "No";
                    row++;
                }

                // Guardar el archivo y devolverlo como una descarga
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                string excelName = $"Departamentos_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }
}
