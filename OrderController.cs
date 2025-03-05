using FlowerWEB.Data;
using FlowerWEB.Models;
using FlowerWEB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerWEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContent<Order> _orderRepository;

        public OrderController(ApplicationDbContext context, IContent<Order> orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        
        public IActionResult Create()
        {
            
            ViewBag.Users = _context.Users.ToList(); 
            ViewBag.OrderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList(); 
            ViewBag.Deliveries = _context.Deliveries.ToList(); 

            
            var model = new Order
            {
                OrderDate = DateTime.Now, 
                TotalAmount = 0.00M, 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order model)
        {
            if (!ModelState.IsValid)
            {
                
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);  
                }

                
                ViewBag.Users = _context.Users.ToList();
                ViewBag.OrderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
                ViewBag.Deliveries = _context.Deliveries.ToList();

                return View(model);
            }

            try
            {
                
                var user = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);
                var delivery = _context.Deliveries.FirstOrDefault(d => d.DeliveryId == model.DeliveryId);

                if (user == null || delivery == null)
                {
                    ModelState.AddModelError("", "Користувач або доставка не знайдені.");
                    return View(model);
                }

                
                model.User = user;
                model.Delivery = delivery;

                
                _orderRepository.Create(model);

                
                _orderRepository.SaveChanges();

                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Помилка при додаванні замовлення: {ex.Message}");
                
                return View(model);
            }
        }


       
        public IActionResult Edit(int id)
        {
            var order = _context.Orders
                                .Include(o => o.User)
                                .Include(o => o.Delivery)
                                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

           
            ViewBag.Users = _context.Users.ToList();
            ViewBag.OrderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
            ViewBag.Deliveries = _context.Deliveries.ToList();

            return View(order);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order model)
        {
            if (id != model.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model); 
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Orders.Any(o => o.OrderId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = _context.Users.ToList();
            ViewBag.OrderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
            ViewBag.Deliveries = _context.Deliveries.ToList();

            return View(model);
        }

       
        public IActionResult Delete(int id)
        {
            var order = _context.Orders
                                .Include(o => o.User)
                                .Include(o => o.Delivery)
                                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index)); // Перехід до списку замовлень
        }

       
        public IActionResult Index()
        {
            var orders = _context.Orders
                                  .Include(o => o.User)
                                  .Include(o => o.Delivery)
                                  .ToList();

            return View(orders);
        }
    }
}