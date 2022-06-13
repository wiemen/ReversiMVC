using ReversiMvcApp.Data;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using System;

namespace ReversiMvcApp.Logic
{
    public class SpelerLogic : ISpelerLogic
    {
        private ReversiDbContext _context;

        public SpelerLogic(ReversiDbContext context)
        {
            _context = context;
        }

        public Speler GetByID(Guid id)
        {
            return _context.Speler.Find(id);
        }

        public bool Add(Speler speler)
        {
            try
            {
                var h = _context.Speler.Add(speler);
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
