using Actividad4LengProg3.Data;
using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Actividad4LengProg3.Controllers
{
    public class MateriasController : Controller
    {

        private readonly EstudiantesDbContext _context;

        public MateriasController(EstudiantesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Materias.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.Codigo == id);
            if (materia == null) return NotFound();

            return View(materia);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: MateriasController/Create
        public async Task<IActionResult> Registrar(MateriaViewModel materia)
        {
            if (!ModelState.IsValid) return View(materia);

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Materia registrada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return NotFound();

            return View(materia);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, MateriaViewModel materia)
        {
            if (id != materia.Codigo) return NotFound();

            if (!ModelState.IsValid) return View(materia);

            _context.Update(materia);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Materia actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return NotFound();

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Materia eliminada.";
            return RedirectToAction(nameof(Index));
        }
    }
}
