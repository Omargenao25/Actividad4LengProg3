using Actividad4LengProg3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Actividad4LengProg3.Controllers
{
    public class MateriasController : Controller
    {

        private readonly EstudiantesDbContext _context;

        public MateriasController(EstudiantesDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: MateriasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MateriasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MateriasController/Create
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

        // GET: MateriasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MateriasController/Edit/5
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

        // GET: MateriasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MateriasController/Delete/5
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
