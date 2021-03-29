using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
   
    class Speler
    {
        public string Naam
        {
            get;
            set;
        }

        public List<Kaart> Kaarten { get; set; } = new List<Kaart>();


        public bool HeeftNogKaarten {
            get {
                return Kaarten.Count > 0;
            }   
        }

        public Speler(string naam) 
        {
            Naam = naam; 
        }

        public Speler(List<Kaart> kaarten)
        {
            Kaarten = kaarten;
        }



        public Kaart LegKaart() {
            int kaartNr = (new Random()).Next(0, Kaarten.Count);
            Kaart nieuwekaart = Kaarten[kaartNr];
            return nieuwekaart;
        }


    }
}
