using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM_PLUS_PROJECT.Models;
using OfficeOpenXml;

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
        public async Task<IActionResult> Index(string term = null)
        {
            var hRMPlusContext = _context.Nominas.Include(n => n.IdDeduccionNavigation).Include(n => n.IdEmpleadoNavigation).Include(n => n.IdTransaccionNavigation);
            //return View(await hRMPlusContext.ToListAsync());
            return View(await hRMPlusContext.Where(x => term == null ||
                                            x.IdNomina.ToString().StartsWith(term)
                                            || x.IdEmpleadoNavigation.Nombre.Contains(term)
                                            || x.IdEmpleadoNavigation.Apellido.Contains(term)
                                            || x.IdDeduccionNavigation.Nombre.Contains(term)
                                            || x.IdTransaccionNavigation.IdTipoTransaccionNavigation.Nombre.Contains(term)
                                            || x.Monto.ToString().Contains(term)).ToListAsync());
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
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "Nombre");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "FullName");
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
            Empleado empleado = _context.Empleados.Find(nomina.IdEmpleado);
            var nominas = _context.Nominas.Where(x => x.IdEmpleado == nomina.IdEmpleado && x.FechaRegistro == DateTime.Now.Date).ToList();

            int fecha = DateTime.Now.Day;

            if (fecha != 15 && fecha != 30)
            {
                ModelState.AddModelError("IdEmpleado", "No es Fecha de Pago ");
            }

            if (nominas.Any())
            {
                ModelState.AddModelError("IdEmpleado", "Existe una nómina creada para este empleado hoy ");
            }


            if (nomina.Monto < empleado.SalarioMensual)
            {
                ModelState.AddModelError("Monto", "El Monto es menor al Salario Mensual " + empleado.SalarioMensual);
            }

            if (nomina.Monto > empleado.SalarioMensual)
            {
                ModelState.AddModelError("Monto", "El Monto es mayor  al Salario Mensual " + empleado.SalarioMensual);
            }

            nomina.FechaRegistro = DateTime.Now.Date;

            if (ModelState.IsValid)
            {
                _context.Add(nomina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "Descripcion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Apellido", nomina.IdEmpleado);
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
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "Descripcion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Apellido", nomina.IdEmpleado);
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
            ViewData["IdDeduccion"] = new SelectList(_context.TipoDeduccions, "IdDeduccion", "Descripcion", nomina.IdDeduccion);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Apellido", nomina.IdEmpleado);
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

        public ActionResult ExportarExcel(string term)
        {
            var nominas = _context.Nominas.Include(e => e.IdTransaccionNavigation).Include(e => e.IdDeduccionNavigation).Include(e => e.IdEmpleadoNavigation);
            //if (!String.IsNullOrEmpty(term))
            //{
            //    nominas = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Nomina, TipoDeduccion?>)nominas.Where(e => e.IdDeduccionNavigation.Nombre.Contains(term));
            //}

            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Nomina");

            // Encabezados de columnas
            worksheet.Cells[1, 1].Value = "Empleado";
            worksheet.Cells[1, 2].Value = "Deducción";
            worksheet.Cells[1, 3].Value = "Transacción";
            worksheet.Cells[1, 4].Value = "Monto";

            // Datos de empleados
            int row = 2;
            foreach (var nomina in nominas)
            {
                worksheet.Cells[row, 1].Value = nomina.IdEmpleadoNavigation.FullName;
                worksheet.Cells[row, 2].Value = nomina.IdDeduccionNavigation.Nombre;
                worksheet.Cells[row, 3].Value = nomina.IdTransaccionNavigation.IdTipoTransaccion;
                worksheet.Cells[row, 4].Value = nomina.Monto;
                row++;
            }

            // Ajustar ancho de columnas
            worksheet.Cells[1, 1, row - 1, 1].AutoFitColumns();
            worksheet.Cells[1, 2, row - 1, 2].AutoFitColumns();
            worksheet.Cells[1, 3, row - 1, 3].AutoFitColumns();
            worksheet.Cells[1, 4, row - 1, 4].AutoFitColumns();

            // Configurar respuesta HTTP
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "nominas.xlsx";
            return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            return View();
        }
    }
}
