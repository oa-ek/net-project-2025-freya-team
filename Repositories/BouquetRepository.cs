using System.Collections.Generic;
using System.Linq;
using FREYA_WEB.Data;
using FREYA_WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace FREYA_WEB.Repositories
{
    public class BouquetRepository : IContent<Bouquet>
    {
        private readonly ApplicationDbContext _context;

        public BouquetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Bouquet entity)
        {
            _context.Bouquets.Add(entity);
            _context.SaveChanges(); // Без цього зміни не збережуться в базі!
        }


        public IEnumerable<Bouquet> Get()
        {
            return _context.Bouquets
                .Include(b => b.Wrapper)
                .Include(b => b.Postcard)
                .ToList();
        }

        public Bouquet? Get(int id)
        {
            return _context.Bouquets
                .Include(b => b.Wrapper)
                .Include(b => b.Postcard)
                .FirstOrDefault(b => b.BouquetId == id);
        }

        public void Update(Bouquet entity)
        {
            _context.Bouquets.Update(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var bouquet = Get(id);
            if (bouquet != null)
            {
                _context.Bouquets.Remove(bouquet);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
