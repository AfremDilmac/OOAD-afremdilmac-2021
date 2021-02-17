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
            int aantalSpaties = 0;
            Console.Write("Geef een tekst: ");
            naam = Convert.ToString(Console.ReadLine());

            for (int i = 0; i < naam.Length; i++)
            {
                if (naam[i] == 'A' || naam[i] == 'E' || naam[i] == 'I' || naam[i] == 'O' || naam[i] == 'U' || naam[i] == 'a' || naam[i] == 'e' || naam[i] == 'i' || naam[i] == 'o' || naam[i] == 'u')
                {
                    aantalKlinkers++;
                }
            }
           
            char letter = Convert.ToChar(naam);
            letter++;
            string naamVerandering = naam;



            Console.WriteLine($"deze tekst bevat {aantalKlinkers} klinkers");
            Console.WriteLine($"{naamVerandering}");
            Console.ReadKey();
        }
    }
}
