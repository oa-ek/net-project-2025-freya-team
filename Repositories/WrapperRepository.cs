using System.Collections.Generic;
using System.Linq;
using FREYA_WEB.Data;
using FREYA_WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace FREYA_WEB.Repositories
{
    public class WrapperRepository : IContent<Wrapper>
    {
        private readonly ApplicationDbContext _context;

        public WrapperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Wrapper entity)
        {
            _context.Wrappers.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Wrapper> Get()
        {
            return _context.Wrappers.Include(w => w.Bouquets).ToList();
        }

        public Wrapper? Get(int id)
        {
            return _context.Wrappers.Include(w => w.Bouquets).FirstOrDefault(w => w.WrapperId == id);
        }

        public void Update(Wrapper entity)
        {
            _context.Wrappers.Update(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var wrapper = Get(id);
            if (wrapper != null)
            {
                _context.Wrappers.Remove(wrapper);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
