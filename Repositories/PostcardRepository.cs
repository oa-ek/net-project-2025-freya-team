using System;
using FREYA_WEB.Data;
using FREYA_WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace FREYA_WEB.Repositories
{
    public class PostcardRepository : IContent<Postcard>
    {
        private readonly ApplicationDbContext _context;

        public PostcardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Postcard entity)
        {
            _context.Postcards.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Postcard> Get()
        {
            return _context.Postcards.Include(p => p.Bouquets).ToList();
        }

        public Postcard? Get(int id)
        {
            return _context.Postcards.Include(p => p.Bouquets).FirstOrDefault(p => p.PostcardId == id);
        }

        public void Update(Postcard entity)
        {
            _context.Postcards.Update(entity);
            SaveChanges();
        }



        public void Delete(int id)
        {
            var postcard = Get(id);
            if (postcard != null)
            {
                _context.Postcards.Remove(postcard);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
