using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    class Deck
    {
        public List<Kaart> Kaarten { get; set; } = new List<Kaart>();

        public Deck()
        {
            string[] kleuren = { "C", "D", "H", "S" };
            for (int i = 0; i < 13; i++)
            {
                foreach (string kleur in kleuren)
                {
                    Kaarten.Add(new Kaart(i + 1, kleur));
                }
            }
        }

        public Kaart NeemKaart()
        {
            return Kaarten.Last();

        }
        public void Shuffle()
        {
            Random rnd = new Random();
            var randomized = Kaarten.OrderBy(item => rnd.Next());
        }
    }
}
