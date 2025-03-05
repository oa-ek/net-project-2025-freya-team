using FlowerWEB.Models;
using FlowerWEB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlowerWEB.Data;

namespace FlowerWEB.Controllers
{
    public class FlowersForOrderController : Controller
    {
        private readonly IContent<FlowersForOrder> _flowersForOrderRepo;
        private readonly ApplicationDbContext _context;

        public FlowersForOrderController(IContent<FlowersForOrder> flowersForOrderRepo, ApplicationDbContext context)
        {
            _flowersForOrderRepo = flowersForOrderRepo;
            _context = context;
        }

        
        public IActionResult Index()
        {
            var flowersForOrders = _flowersForOrderRepo.Get();
            return View(flowersForOrders);
        }

        public IActionResult Create()
        {
            
            ViewBag.Flowers = new SelectList(_context.Flowers, "FlowerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FlowersForOrder flowersForOrder)
        {
            if (ModelState.IsValid)
            {
                _flowersForOrderRepo.Create(flowersForOrder);
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Flowers = new SelectList(_context.Flowers, "FlowerId", "Name", flowersForOrder.FlowerId);
            return View(flowersForOrder);
        }

        public IActionResult Edit(int id)
        {
            var flowersForOrder = _flowersForOrderRepo.Get(id);
            if (flowersForOrder == null) return NotFound();

            ViewBag.Flowers = new SelectList(_context.Flowers, "FlowerId", "Name", flowersForOrder.FlowerId);
            return View(flowersForOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FlowersForOrder flowersForOrder)
        {
            if (id != flowersForOrder.FlowOId) return NotFound();

            if (ModelState.IsValid)
            {
                _flowersForOrderRepo.Update(flowersForOrder);
                return RedirectToAction(nameof(Index));
            }
           
            ViewBag.Flowers = new SelectList(_context.Flowers, "FlowerId", "Name", flowersForOrder.FlowerId);
            return View(flowersForOrder);
        }

       
        public IActionResult Delete(int id)
        {
            var flowersForOrder = _flowersForOrderRepo.Get(id);
            if (flowersForOrder == null) return NotFound();
            return View(flowersForOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _flowersForOrderRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
