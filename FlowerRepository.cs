using FlowerWEB.Data;
using FlowerWEB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FlowerWEB.Repositories
{
    public class FlowerRepository : IContent<Flower>
    {
        private readonly ApplicationDbContext _context;

        public FlowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Flower entity)
        {
            _context.Flowers.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Flower> Get()
        {
            return _context.Flowers.Include(f => f.Formings).Include(f => f.FlowersForOrders).ToList();
        }

        public Flower? Get(int id)
        {
            return _context.Flowers.Include(f => f.Formings).Include(f => f.FlowersForOrders)
                .FirstOrDefault(f => f.FlowerId == id);
        }

        public void Update(Flower entity)
        {
            _context.Flowers.Update(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var flower = Get(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
