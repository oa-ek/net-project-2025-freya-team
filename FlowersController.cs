using FlowerWEB.Models;
using FlowerWEB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlowerWEB.Controllers
{
    public class FlowersController : Controller
    {
        private readonly IContent<Flower> _flowerRepo;

        public FlowersController(IContent<Flower> flowerRepo)
        {
            _flowerRepo = flowerRepo;
        }

        public IActionResult Index()
        {
            var flowers = _flowerRepo.Get();
            return View(flowers);
        }

        public IActionResult Details(int id)
        {
            var flower = _flowerRepo.Get(id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flower flower)
        {
            if (ModelState.IsValid)
            {
                _flowerRepo.Create(flower);
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        public IActionResult Edit(int id)
        {
            var flower = _flowerRepo.Get(id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Flower flower)
        {
            if (ModelState.IsValid)
            {
                _flowerRepo.Update(flower);
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        public IActionResult Delete(int id)
        {
            var flower = _flowerRepo.Get(id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _flowerRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
