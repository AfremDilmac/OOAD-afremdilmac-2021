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
            Kaart nummer = new Kaart();
       
            Console.WriteLine($"{nummer.KaartNummer}");
            Console.ReadLine();
        }
    }
}
