using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBankautomaat
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //Declaratie
            int saldo = 500;
            int pincode;
            string keuze;
            
            //Loop pincode
            do
            {
                Console.WriteLine("Bankautomaat");
                Console.WriteLine("============");
                Console.WriteLine("");
                Console.Write("Geef uw pincode in: ");
                pincode = Convert.ToInt32(Console.ReadLine());
            } while (pincode != 1111);

            //Keuze van operatie
            Start:
            Console.WriteLine(" a.afhaling \n b. storting \n c. stoppen \n je keuze: ");
            keuze = Convert.ToString(Console.ReadLine());
            int nieuwesaldo;

            //Operaties binnen een do while
            do
            {
                if (keuze == "a"  )
                {
                    Retry:
                    Console.WriteLine("Welk bedrag wil je afhalen: ");
                    int afhalen = Convert.ToInt32(Console.ReadLine());
                    nieuwesaldo = Convert.ToInt32(saldo - afhalen);
                    if (nieuwesaldo < 0)
                    {
                        Console.WriteLine("Fout! u kunt niet onder de saldo gaan");
                        goto Retry;
                    }
                    else
                    {
                        saldo = nieuwesaldo;
                        Console.WriteLine($"Afhaling ok - het nieuwe saldo is {saldo}");
                        goto Start;
                    }
                 
                }
                else if (keuze == "b")
                {
                Retry:
                    Console.WriteLine("Welk bedrag wil je storten: ");
                    int storten = Convert.ToInt32(Console.ReadLine());
                    nieuwesaldo = Convert.ToInt32(saldo + storten);
                    if (nieuwesaldo < 0)
                    {
                        Console.WriteLine("Fout! u kunt niet onder de saldo gaan");
                        goto Retry;
                    }
                    else
                    {
                        saldo = nieuwesaldo;
                        Console.WriteLine($"Afhaling ok - het nieuwe saldo is {saldo}");
                        goto Start;
                    }

                }
                else if (keuze == "c")
                {
                    Console.WriteLine("Bedankt en tot ziens.");
                }
                else
                {
                    Console.WriteLine("\n Ongeldige keuze \n a.afhaling \n b. storting \n c. stoppen \n je keuze: ");
                    keuze = Convert.ToString(Console.ReadLine());
                }

            } while (keuze != "c");

            Console.ReadKey();

        }




    }
}

