using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.Context;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;

namespace Aon18.data.Repository
{
    public class SkeletDbRepository : ISkeletDbRepository
    {
        private readonly ExamDbContext _context;

        public SkeletDbRepository(ExamDbContext context)
        {
            _context = context;
        }

        public Skelet AddSkelet(Skelet skelet)
        {
            _context.Skelets.Add(skelet);
            _context.SaveChanges();
            return skelet;
        }

        public void DeleteById(int id)
        {
            var skelet = _context.Skelets.Find(id);
            _context.Skelets.Remove(skelet);
            _context.SaveChanges();
        }

        public IEnumerable<Skelet> GetAllSkelets()
        {
            return _context.Skelets.ToList();
        }

        public Skelet GetById(int id)
        {
            return _context.Skelets.Find(id);
        }
    }
}
