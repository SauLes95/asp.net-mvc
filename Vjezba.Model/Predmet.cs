using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Predmet
    {
        public Predmet() { }

        public int Sifra {  get; set; }
        public int ECTS { get; set; }
        public string Naziv {  get; set; }
    }
}
