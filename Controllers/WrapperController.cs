using FREYA_WEB.Models;
using FREYA_WEB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FREYA_WEB.Controllers
{
    public class WrapperController : Controller
    {
        private readonly IContent<Wrapper> _wrapperRepo;

        public WrapperController(IContent<Wrapper> wrapperRepo)
        {
            _wrapperRepo = wrapperRepo;
        }

        public IActionResult Index()
        {
            var wrappers = _wrapperRepo.Get();
            return View(wrappers);
        }

        public IActionResult Details(int id)
        {
            var wrapper = _wrapperRepo.Get(id);
            if (wrapper == null) return NotFound();
            return View(wrapper);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Wrapper wrapper)
        {
            if (ModelState.IsValid)
            {
                _wrapperRepo.Create(wrapper);
                return RedirectToAction(nameof(Index));
            }
            return View(wrapper);
        }

        public IActionResult Edit(int id)
        {
            var wrapper = _wrapperRepo.Get(id);
            if (wrapper == null) return NotFound();
            return View(wrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Wrapper wrapper)
        {
            if (ModelState.IsValid)
            {
                _wrapperRepo.Update(wrapper);
                return RedirectToAction(nameof(Index));
            }
            return View(wrapper);
        }

        public IActionResult Delete(int id)
        {
            var wrapper = _wrapperRepo.Get(id);
            if (wrapper == null) return NotFound();
            return View(wrapper);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _wrapperRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
