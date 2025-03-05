using FlowerWEB.Data;
using FlowerWEB.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerWEB.Repositories
{
    public class OrderRepository : IContent<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Order entity)
        {
            _context.Orders.Add(entity); 
            SaveChanges(); 
        }


        public IEnumerable<Order> Get()
        {
            return _context.Orders
                .Include(o => o.User)        
                .Include(o => o.Delivery)     
                .Include(o => o.FlowersForOrders) 
                .Include(o => o.BouquetsForOrders) 
                .ToList();
        }

        public Order? Get(int id)
        {
            return _context.Orders
                .Include(o => o.User)        
                .Include(o => o.Delivery)     
                .Include(o => o.FlowersForOrders) 
                .Include(o => o.BouquetsForOrders)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public void Update(Order entity)
        {
            _context.Orders.Update(entity);
        }

        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
