using System;

namespace ReversiMvcApp.Models
{
    public class Speler
    {
        public Guid ID { get; set; }
        public string Naam { get; set; }
        public int AantalGewonnen { get; set; }
        public int AantalVerloren { get; set; }
        public int AantalGelijk { get; set; }
    }
}
