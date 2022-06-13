using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;

namespace ReversiMvcApp.Logic.Interfaces
{
    public interface ISpelLogic
    {
        List<Spel> GetAll();
        Spel Get(string id);
        bool Add(Spel spel);
        bool Update(Spel spel);
        Spel Join(Guid spelID, string spelerID);
        Spel GetBySpelerID(string token);
        List<Spel> GetWachtendeSpellen();
    }
}
