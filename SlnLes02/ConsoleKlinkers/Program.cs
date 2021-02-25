using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKlinkers
{
    class Program
    {
        static void Main(string[] args)
        {
            string naam;
            int aantalKlinkers = 0;
            int letters = 0;            
            Console.Write("Geef een tekst: ");
            naam = Convert.ToString(Console.ReadLine());

            //Loop dat check of er in de invoer een klinker is en +1 per klinker.
            for (int i = 0; i < naam.Length; i++)
            {
                if (naam[i] == 'A' || naam[i] == 'E' || naam[i] == 'I' || naam[i] == 'O' || naam[i] == 'U' || naam[i] == 'a' || naam[i] == 'e' || naam[i] == 'i' || naam[i] == 'o' || naam[i] == 'u')
                {
                    aantalKlinkers++;
                }
            }

            Console.WriteLine($"deze tekst bevat {aantalKlinkers} klinkers");

            Console.Write("Jouw geheimnaam is: ");

            //Voor elk character in een word +1 (dus naar volgende letter a wordt b)
            foreach (char ch in naam)
            {

                char bewerking = Convert.ToChar(ch + 1);

                string geheim = Convert.ToString(bewerking);

                Console.Write(geheim);
            }

            //telt lengte van woorden
            int lengte = naam.Length;

            Console.WriteLine($"{Environment.NewLine}Uw tekst heeft een totaal van {lengte} letters.");
            Console.ReadKey();
        }
    }
}
