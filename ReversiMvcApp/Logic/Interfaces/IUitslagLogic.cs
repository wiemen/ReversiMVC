using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;

namespace ReversiMvcApp.Logic.Interfaces
{
    public interface IUitslagLogic
    {
        void Add(Uitslag spel);

        public bool Update(Uitslag spel);

        public List<Uitslag> GetAll();

        Uitslag Get(Guid ID);
    }
}
