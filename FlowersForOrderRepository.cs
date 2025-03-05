using FlowerWEB.Data;
using FlowerWEB.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerWEB.Repositories
{
    public class FlowersForOrderRepository : IContent<FlowersForOrder>
    {
        private readonly ApplicationDbContext _context;

        public FlowersForOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(FlowersForOrder entity)
        {
            _context.FlowersForOrders.Add(entity);
            SaveChanges();
        }

        public IEnumerable<FlowersForOrder> Get()
        {
            return _context.FlowersForOrders
                .Include(f => f.Flower)   
                .Include(f => f.Order)    
                .ToList();
        }

        public FlowersForOrder? Get(int id)
        {
            return _context.FlowersForOrders
                .Include(f => f.Flower)
                .Include(f => f.Order)
                .FirstOrDefault(f => f.FlowOId == id);
        }

        public void Update(FlowersForOrder entity)
        {
            _context.FlowersForOrders.Update(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var flowersForOrder = Get(id);
            if (flowersForOrder != null)
            {
                _context.FlowersForOrders.Remove(flowersForOrder);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
