using ReversiMvcApp.Models.Enums;
using System;

namespace ReversiMvcApp.Models
{
    public class Uitslag
    {
        public Guid ID { get; set; }
        public Guid SpelID { get; set; }
        public Kleur Winnaar { get; set; }
        public int PuntenWit { get; set; }
        public int PuntenZwart { get; set; }
        public string Speler1Token { get; set; }
        public string Speler2Token { get; set; }

        public bool Opgegeven { get; set; }
        public string Opgever { get; set; }

        public Uitslag() { }

        public Uitslag(int puntenWit, int puntenZwart, Spel spel, Kleur winnaar)
        {
            ID = Guid.NewGuid();
            SpelID = new Guid(spel.Token);
            Winnaar = winnaar;
            PuntenWit = puntenWit;
            PuntenZwart = puntenZwart;
            Speler1Token = spel.Speler1Token;
            Speler2Token = spel.Speler2Token;
        }
    }
}
