using Actividad4LengProg3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Actividad4LengProg3.Controllers
{
    public class CalificacionesController : Controller
    {

        private readonly EstudiantesDbContext _context;

        public CalificacionesController(EstudiantesDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: CalificacionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalificacionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalificacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalificacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalificacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalificacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalificacionesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
