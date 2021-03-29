using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int AANTAL_KAARTEN_HAND = 5;
            Deck deck = new Deck();
            deck.Shuffle();
            Speler spelerHans = new Speler("Hans");
            Speler spelerRogier = new Speler("Rogier");
            // deel kaarten uit
            for (int i = 0; i < AANTAL_KAARTEN_HAND; i++)
            {
                spelerHans.Kaarten.Add(deck.NeemKaart());
                spelerRogier.Kaarten.Add(deck.NeemKaart());
            }
            // speel spel
            double puntenHans = 0;
            double puntenRogier = 0;
            while (spelerHans.HeeftNogKaarten && spelerRogier.HeeftNogKaarten)
            {
                Kaart kaart1 = spelerHans.LegKaart();
                Kaart kaart2 = spelerRogier.LegKaart();
                Console.WriteLine($"Hans legt {kaart1.KaartNummer}{kaart1.KaartNummer}");
                Console.WriteLine($"Rogier legt {kaart2.KaartNummer}{kaart2.KaartNummer}");
                if (kaart1.KaartNummer > kaart2.KaartNummer) puntenHans++;
                else if (kaart1.KaartNummer < kaart2.KaartNummer) puntenRogier++;
                else
                {
                    puntenRogier += 0.5;
                    puntenHans += 0.5;
                }
               
                Console.WriteLine($"stand: Hans {puntenHans} - Rogier {puntenRogier}");
            }
            // einde
            if (puntenRogier == puntenHans)
            {
                Console.WriteLine($"{Environment.NewLine}gelijkspel!");
            }
            else if (puntenRogier > puntenHans)
            {
                Console.WriteLine($"{Environment.NewLine}Rogier wint!");
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Hans wint!");

            }
            Console.ReadKey();
        }
    }
}
