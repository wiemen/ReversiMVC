using ReversiMvcApp.Models.Enums;
using System;

namespace ReversiMvcApp.Models
{
    public class Spel
    {
        private const int bordOmvang = 8;
        private Kleur[,] bord;

        public Guid ID { get; set; }
        public string Token { get; set; }
        public string Speler1Token { get; set; }
        public string Speler1Naam { get; set; }
        public string Speler2Token { get; set; }
        public string Omschrijving { get; set; }
        public Kleur AandeBeurt { get; set; }
        public Status Status { get; set; }
        public Kleur[,] Bord
        {
            get { return bord; }
            set { bord = value; }
        }

        public Spel()
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Token = Token.Replace("/", "q");    // slash mijden ivm het opvragen van een spel via een api obv het token
            Token = Token.Replace("+", "r");    // plus mijden ivm het opvragen van een spel via een api obv het token

            Bord = new Kleur[bordOmvang, bordOmvang];
            Bord[3, 3] = Kleur.Wit;
            Bord[4, 4] = Kleur.Wit;
            Bord[3, 4] = Kleur.Zwart;
            Bord[4, 3] = Kleur.Zwart;

            AandeBeurt = Kleur.Geen;
        }
    }
}
