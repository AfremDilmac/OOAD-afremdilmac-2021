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
            double saldo = 500;
            int pincode;
            string keuze;

            //Loop pincode (Hier heb ik pincode als loop ingesteld gebruiker mag zelf pincode kiezen.)
            Console.WriteLine("Kies een pincode: ");
            int IngevoerdePincode = Convert.ToInt32(Console.ReadLine());

            do
            {
                Console.WriteLine("Bankautomaat");
                Console.WriteLine("============ \n");
                Console.Write("Geef uw pincode in: ");
                pincode = Convert.ToInt32(Console.ReadLine());
                if (pincode != IngevoerdePincode)
                {
                    Console.WriteLine("Fout! probeer opnieuw \n");
                }
            } while (pincode != IngevoerdePincode);

            //Operaties binnen een do while
            //u kunt altijd kiezen tot dat u c kiest om te stoppen
            do
            {
                //Keuze van operatie
                Console.WriteLine("\n a. afhaling \n b. storting \n c. stoppen \n je keuze: ");
                keuze = Convert.ToString(Console.ReadLine());
                double nieuwesaldo;

                //Hier kan men een bedrag afhalen
                if (keuze == "a"  )
                {
                    Console.WriteLine("Welk bedrag wil je afhalen: ");
                    double afhalen = Convert.ToDouble(Console.ReadLine());
                    nieuwesaldo = Convert.ToDouble(saldo - afhalen);
                    if (nieuwesaldo < 0)
                    {
                        Console.WriteLine("Fout! u kunt niet onder de saldo gaan");
                    }
                    else
                    {
                        saldo = nieuwesaldo;
                        Console.WriteLine($"Afhaling ok - het nieuwe saldo is {saldo}");
                    }
                }
                //Hier kan men een bedrag instorten
                else if (keuze == "b")
                {
                    Console.WriteLine("Welk bedrag wil je storten: ");
                    double storten = Convert.ToDouble(Console.ReadLine());
                    nieuwesaldo = Convert.ToDouble(saldo + storten);
                    saldo = nieuwesaldo;
                    Console.WriteLine($"Afhaling ok - het nieuwe saldo is {saldo}");
                }
                else if (keuze == "c")
                {
                    Console.WriteLine("Bedankt en tot ziens.");
                }
                else
                {
                    Console.WriteLine("\n Ongeldige keuze \n a. afhaling \n b. storting \n c. stoppen \n je keuze: ");
                    keuze = Convert.ToString(Console.ReadLine());
                }

            } while (keuze != "c");

            Console.ReadKey();

        }




    }
}

