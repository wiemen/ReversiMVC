using ReversiMvcApp.Models;
using System;

namespace ReversiMvcApp.Logic.Interfaces
{
    public interface ISpelerLogic
    {
        Speler GetByID(Guid id);
        bool Add(Speler speler);
    }
}
