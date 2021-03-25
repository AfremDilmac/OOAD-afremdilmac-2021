using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    class Kaart
    {
        //Declaratie
        private int _Nummer;
        private string _Kleur;

        public int KaartNummer
        {
            get { return _Nummer; }
            set {
                if (value < 1 || value > 13){
                    _Nummer = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} moet tussen 1 en 13 liggen");
                }
                
            }
        }

        public string KaartKleur
        {
            get { return _Kleur; }
            set
            {
                if (value == "C" || value == "S" || value == "H" || value == "D")
                {
                    _Kleur = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} moet c / s / h / d zijn");
                }
            }

        }

        public Kaart(int nummer, string kleur) {
            KaartNummer = nummer;
            KaartKleur = kleur;
        }
    }
}
