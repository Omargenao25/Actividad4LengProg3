using Actividad4LengProg3.Data;
using Actividad4LengProg3.Models;
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

        public IActionResult Lista()
        {
            var calificaciones = _context.Calificaciones
                .Include(c => c.Estudiantes)
                .Include(c => c.Materia);
            return View( calificaciones.ToList());
        }

        [HttpPost]
        public IActionResult Registrar(CalificacionViewModel calificacion)
        {
            if (ModelState.IsValid)
            {
                var calificaciones = new CalificacionViewModel() 
                {
                    Materia = calificacion.Materia,
                    CodigoMateria = calificacion.CodigoMateria,
                    Nota = calificacion.Nota,
                    Periodo = calificacion.Periodo,
                    Estudiante = calificacion.Estudiante   

                };
                _context.Calificaciones .Add (calificaciones);
            }

            _context.Calificaciones.Add(calificacion);
            _context.SaveChanges();
            TempData["Mensaje"] = "Calificación registrada correctamente.";
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var calificacion =  _context.Calificaciones.Find(id);
            if (calificacion == null) return NotFound();

            ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "Matricula", calificacion.MatriculaEstudiante);
            ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Codigo", calificacion.CodigoMateria);
            return View(calificacion);
        }

        [HttpPost]
        public IActionResult Editar(int id, CalificacionViewModel calificacion)
        {
            if (id != calificacion.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "Matricula", "Matricula", calificacion.MatriculaEstudiante);
                ViewBag.Materias = new SelectList(_context.Materias, "Codigo", "Codigo", calificacion.CodigoMateria);
                return View(calificacion);
            }

            _context.Update(calificacion);
            _context.SaveChanges();
            TempData["Mensaje"] = "Calificación actualizada correctamente.";
            return RedirectToAction(nameof(Lista));
        }

        public  IActionResult Eliminar(int id)
        {
            var calificacion =  _context.Calificaciones.Find(id);
            if (calificacion == null) return NotFound();

            _context.Calificaciones.Remove(calificacion);
            _context.SaveChanges();
            TempData["Mensaje"] = "Calificación eliminada.";
            return RedirectToAction(nameof(Lista));
        }
    }
}
