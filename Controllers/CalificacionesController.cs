using Actividad4LengProg3.Data;
using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Actividad4LengProg3.Controllers
{
    public class CalificacionesController : Controller
    {

        private readonly EstudiantesDbContext _context;

        public CalificacionesController(EstudiantesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lista()
        {
            var calificaciones = _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia);
            return View(await calificaciones.ToListAsync());
        }

        // GET: CalificacionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "NombreCompleto");
            ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Nombre");
            return View("Registrar");

        }

        [HttpPost]
        public async Task<IActionResult> Registrar(CalificacionViewModel calificacion)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "NombreCompleto", calificacion.MatriculaEstudiante);
                ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Nombre", calificacion.CodigoMateria);
                return View("Registrar", calificacion);

            }

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Calificación registrada correctamente.";
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]   
        public async Task<IActionResult> Editar(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null) return NotFound();

            ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "NombreCompleto", calificacion.MatriculaEstudiante);
            ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Nombre", calificacion.CodigoMateria);
            return View(calificacion);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CalificacionViewModel calificacion)
        {
            if (id != calificacion.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "NombreCompleto", calificacion.MatriculaEstudiante);
                ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Nombre", calificacion.CodigoMateria);
                return View(calificacion);
            }

            _context.Update(calificacion);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Calificación actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null) return NotFound();

            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Calificación eliminada.";
            return RedirectToAction(nameof(Index));
        }
    }
}
