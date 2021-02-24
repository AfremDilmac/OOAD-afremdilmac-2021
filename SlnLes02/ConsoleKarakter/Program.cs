using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKarakter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaratie 
            Console.Write("Geef een kleine letter: ");
            char invoer = Convert.ToChar(Console.ReadLine());
            Console.WriteLine($"Het nummer is {Convert.ToInt32(invoer)}");
            invoer = Convert.ToChar(invoer);

            //Om een hoofdletter te krijgen invoer geconverteerd in char - 32
            Console.WriteLine($"De hoofdletter is {Convert.ToChar(invoer - 32)}");

            //Vorige letter = char - 1 
            invoer = (Convert.ToChar(invoer - 1));
            Console.WriteLine($"De vorige letter is {Convert.ToChar(invoer)}");

            //Volgende letter = char + 1 (maar omdat het al in -1 was is het +2)
            Console.WriteLine($"De volgende letter is {Convert.ToChar(invoer + 2)}");

            Console.ReadKey();

        }
    }
}
