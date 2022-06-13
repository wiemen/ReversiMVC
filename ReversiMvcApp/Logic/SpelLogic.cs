using ReversiMvcApp.Data;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using ReversiMvcApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversiMvcApp.Logic
{
    public class SpelLogic : ISpelLogic
    {
        private ReversiDbContext _context;

        public SpelLogic(ReversiDbContext context)
        {
            _context = context;
        }

        public List<Spel> GetAll()
        {
            return _context.Spel.ToList();
        }

        public Spel Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return _context.Spel.Find(new Guid(id));
        }

        public Spel Join(Guid spelID, string spelerID)
        {
            var spel = _context.Spel.Find(spelID);
            if (spel != null && string.IsNullOrEmpty(spel.Speler2Token))
            {
                spel.Speler2Token = spelerID;
                spel.AandeBeurt = Kleur.Wit;
                spel.Status = Status.Bezig;

                if (Update(spel))
                {
                    return spel;
                }
            }

            return null;
        }

        public bool Add(Spel spel)
        {
            spel.ID = Guid.NewGuid();
            spel.Token = spel.ID.ToString();
            try
            {
                _context.Spel.Add(spel);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Spel spel)
        {
            try
            {
                var index = _context.Spel.Update(spel);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Spel GetBySpelerID(string token)
        {
            var spel = _context.Spel.FirstOrDefault(x => (x.Speler1Token == token || x.Speler2Token == token) &&
            (x.Status == Status.Bezig || x.Status == Status.Wachtende));

            return spel;
        }

        public List<Spel> GetWachtendeSpellen()
        {
            var spellen = _context.Spel.Where(x => string.IsNullOrEmpty(x.Speler2Token));
            return spellen.ToList();
        }
    }
}
