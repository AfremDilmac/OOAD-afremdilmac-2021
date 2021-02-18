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
            Console.WriteLine($"De hoofdletter is {Convert.ToChar(invoer - 32)}");
            invoer = (Convert.ToChar(invoer - 1));
            Console.WriteLine($"De vorige letter is {Convert.ToChar(invoer)}");
            Console.WriteLine($"De volgende letter is {Convert.ToChar(invoer + 2)}");

            Console.ReadKey();

        }
    }
}
