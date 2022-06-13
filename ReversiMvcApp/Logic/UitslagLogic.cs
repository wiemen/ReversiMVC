using ReversiMvcApp.Data;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversiMvcApp.Logic
{
    public class UitslagLogic : IUitslagLogic
    {
        private ReversiDbContext _context;

        public UitslagLogic(ReversiDbContext context)
        {
            _context = context;
        }

        public void Add(Uitslag uitslag)
        {
            _context.Uitslag.Add(uitslag);
            _context.SaveChanges();
        }

        public Uitslag Get(Guid id)
        {
            return _context.Uitslag.FirstOrDefault(x => x.ID == id);
        }

        public List<Uitslag> GetAll()
        {
            return _context.Uitslag.ToList();
        }

        public bool Update(Uitslag uitslag)
        {
            try
            {
                var index = _context.Uitslag.Update(uitslag);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
