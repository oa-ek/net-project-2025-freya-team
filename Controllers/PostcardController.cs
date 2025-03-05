using FREYA_WEB.Models;
using FREYA_WEB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FREYA_WEB.Controllers
{
    public class PostcardController : Controller
    {
        private readonly IContent<Postcard> _postcardRepo;

        public PostcardController(IContent<Postcard> postcardRepo)
        {
            _postcardRepo = postcardRepo;
        }

        // Вивід списку листівок
        public IActionResult Index()
        {
            var postcards = _postcardRepo.Get();
            return View(postcards);
        }

        // GET: Створення листівки
        public IActionResult Create()
        {
            return View();
        }

        // POST: Створення листівки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Postcard postcard)
        {
            if (ModelState.IsValid)
            {
                _postcardRepo.Create(postcard);
                return RedirectToAction(nameof(Index));
            }
            return View(postcard);
        }

        // GET: Редагування
        public IActionResult Edit(int id)
        {
            var postcard = _postcardRepo.Get(id);
            if (postcard == null) return NotFound();
            return View(postcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Postcard postcard)
        {
            if (id != postcard.PostcardId) return NotFound();

            if (ModelState.IsValid)
            {
                _postcardRepo.Update(postcard);
                return RedirectToAction(nameof(Index));
            }
            return View(postcard);
        }

        // GET: Видалення
        public IActionResult Delete(int id)
        {
            var postcard = _postcardRepo.Get(id);
            if (postcard == null) return NotFound();
            return View(postcard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _postcardRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
