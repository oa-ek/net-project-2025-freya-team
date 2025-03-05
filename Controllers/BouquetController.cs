using FREYA_WEB.Models;
using FREYA_WEB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FREYA_WEB.Controllers
{
    public class BouquetController : Controller
    {
        private readonly IContent<Bouquet> _bouquetRepo;
        private readonly IContent<Wrapper> _wrapperRepo;
        private readonly IContent<Postcard> _postcardRepo;

        public BouquetController(IContent<Bouquet> bouquetRepo, IContent<Wrapper> wrapperRepo, IContent<Postcard> postcardRepo)
        {
            _bouquetRepo = bouquetRepo;
            _wrapperRepo = wrapperRepo;
            _postcardRepo = postcardRepo;
        }

        public IActionResult Index()
        {
            var bouquets = _bouquetRepo.Get();
            return View(bouquets);
        }

        public IActionResult Details(int id)
        {
            var bouquet = _bouquetRepo.Get(id);
            if (bouquet == null) return NotFound();
            return View(bouquet);
        }

        public IActionResult Create()
        {
            // Завантажуємо дані для обгорток та листівок
            ViewBag.Wrappers = new SelectList(_wrapperRepo.Get(), "WrapperId", "Name");
            ViewBag.Postcards = new SelectList(_postcardRepo.Get(), "PostcardId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                _bouquetRepo.Create(bouquet); // Додає букет
                return RedirectToAction(nameof(Index)); // Повертає на список букетів
            }

            // Якщо модель не валідна, повторно завантажуємо дані для обгорток та листівок
            ViewBag.Wrappers = new SelectList(_wrapperRepo.Get(), "WrapperId", "Name", bouquet.WrapperId);
            ViewBag.Postcards = new SelectList(_postcardRepo.Get(), "PostcardId", "Name", bouquet.PostcardId);

            return View(bouquet); // Повертає форму з помилками
        }



        public IActionResult Edit(int id)
        {
            var bouquet = _bouquetRepo.Get(id);
            if (bouquet == null) return NotFound();
            return View(bouquet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                _bouquetRepo.Update(bouquet);
                return RedirectToAction(nameof(Index));
            }
            return View(bouquet);
        }

        public IActionResult Delete(int id)
        {
            var bouquet = _bouquetRepo.Get(id);
            if (bouquet == null) return NotFound();
            return View(bouquet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bouquetRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
