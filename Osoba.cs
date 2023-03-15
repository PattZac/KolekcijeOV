using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolekcijeOV
{
    class Osoba
    {
        string ime, prezime, dodatak;
        int godina;
        char rod;

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public int Godina { get => godina; set => godina = value; }
        public char Rod { get => rod; set => rod = value; }
        public string Dodatak { get => dodatak; set => dodatak = value; }

        public override string ToString()
        {
            string text = Ime + "\t\t" + Prezime + "\t\t" + Godina + "\t\t" + rod + "\t\t" + Dodatak;
            return text;
        }
        public string ToCsvString()
        {
            string text = Ime + ", " + Prezime + ", " + Godina + ", " + rod + ", " + Dodatak+ ",";
            return text;
        }
        public Osoba(string i, string p, int g, char r) {
            Ime = i;
            Prezime = p;
            Godina = g;
            Rod = r;
        }
        public void ObradiDodatak()
        {
            if (Rod == 'M')
            {
                Dodatak = "Ima brkove.";
            }
            else
            {
                Dodatak = "Nema brkove.";
            }
        }
    }
}
