using Actividad4LengProg3.Data;
using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Actividad4LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly EstudiantesDbContext _context;

        public EstudiantesController(EstudiantesDbContext context)
        {
            _context = context;
        }

        // Lista de carreras, turnos, tipos de ingreso, etc.
        private List<string> carreras = new List<string> { "Ingeniería", "Administración", "Sistemas", "Contabilidad" };
        private List<string> generos = new List<string> { "Masculino", "Femenino", "Otro" };
        private List<string> turnos = new List<string> { "Mañana", "Tarde", "Noche" };
        private List<string> tiposIngreso = new List<string> { "Regular", "transferido", "Reingreso" };

        public ActionResult Index()
        {
            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(EstudianteViewModel estudiante)
        {
            if (_context.Estudiantes.Any(e => e.Matricula == estudiante.Matricula))
            {
                ModelState.AddModelError("Matricula", "La matrícula ya existe.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = carreras;
                ViewBag.Generos = generos;
                ViewBag.Turnos = turnos;
                ViewBag.TiposIngreso = tiposIngreso;
                return View("Index", estudiante);
            }

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Estudiante registrado exitosamente.";
            return RedirectToAction("Lista");
        }

        public async Task<IActionResult> Lista()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return View(estudiantes);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                TempData["Error"] = "Matrícula inválida.";
                return RedirectToAction("Lista");
            }

            var estudiante = await _context.Estudiantes.FindAsync(matricula);
            if (estudiante == null)
            {
                TempData["Error"] = "Estudiante no encontrado.";
                return RedirectToAction("Lista");
            }

            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;

            return View("Index", estudiante);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(EstudianteViewModel estudiante)
        {
            var estudianteExistente = await _context.Estudiantes.FindAsync(estudiante.Matricula);
            if (estudianteExistente == null)
            {
                TempData["Error"] = "No se encontró el estudiante a editar.";
                return RedirectToAction("Lista");
            }

            if (estudiante.EstaBecado && (!estudiante.PorcentajeBeca.HasValue || estudiante.PorcentajeBeca <= 0))
            {
                ModelState.AddModelError("PorcentajeBeca", "Debe especificar un porcentaje válido.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = carreras;
                ViewBag.Generos = generos;
                ViewBag.Turnos = turnos;
                ViewBag.TiposIngreso = tiposIngreso;
                return View("Index", estudiante);
            }

            estudianteExistente.NombreCompleto = estudiante.NombreCompleto;
            estudianteExistente.Carrera = estudiante.Carrera;
            estudianteExistente.CorreoInstitucional = estudiante.CorreoInstitucional;
            estudianteExistente.Telefono = estudiante.Telefono;
            estudianteExistente.FechaNacimiento = estudiante.FechaNacimiento;
            estudianteExistente.Genero = estudiante.Genero;
            estudianteExistente.Turno = estudiante.Turno;
            estudianteExistente.TipoIngreso = estudiante.TipoIngreso;
            estudianteExistente.EstaBecado = estudiante.EstaBecado;
            estudianteExistente.PorcentajeBeca = estudiante.PorcentajeBeca;
            estudianteExistente.AceptaTerminos = estudiante.AceptaTerminos;

            _context.Update(estudianteExistente);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Estudiante actualizado exitosamente.";
            return RedirectToAction("Lista");
        }


        public async Task<IActionResult> Eliminar(string matricula)
        {
            var estudiante = await _context.Estudiantes.FindAsync(matricula);

            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Estudiante eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "Estudiante no encontrado.";
            }

            return RedirectToAction("Lista");
        }
    }
}

