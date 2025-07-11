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

        public IActionResult Index()
        {
          ViewBag.Estudiantes = _context.Estudiantes
            .Select(u => new SelectListItem
            {
                Value = u.Matricula,
                Text = u.Matricula + " - " + u.NombreCompleto + " - " + u.Carrera
            }).ToList();

            ViewBag.Materias = _context.Materias.Select(x=> new SelectListItem
            {
                Value = x.Codigo,
                Text = x.Codigo + " - " + x.Nombre
            }).ToList();

            return View(new CalificacionViewModel());
        }

        public IActionResult Lista()
        {
            var calificaciones = _context.Calificaciones.ToList();
            return View (calificaciones);
        }

        [HttpPost]
        public IActionResult Registrar(CalificacionViewModel calificacion)
        {
            if (ModelState.IsValid)
            {
                var calificaciones = new CalificacionViewModel()
                {
                    MatriculaEstudiante = calificacion.MatriculaEstudiante,
                    CodigoMateria = calificacion.CodigoMateria,
                    Nota = calificacion.Nota,
                    Periodo = calificacion.Periodo

                };
                _context.Calificaciones.Add(calificaciones);
                _context.SaveChanges();
                TempData["Mensaje"] = "Calificación registrada correctamente.";
                return RedirectToAction(nameof(Lista));
            }
            return View(calificacion);
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
