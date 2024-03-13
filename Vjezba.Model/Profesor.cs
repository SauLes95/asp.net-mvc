using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public enum ZvanjeProf
    {
        Asistent,
        Predavac,
        VisiPredavac,
        ProfVisokeSkole
    }
    public class Profesor : Osoba
    {
        public Profesor() { }

        public string Odjel { get; set; }

        public DateTime DatumIzbora { get; set; }

        public ZvanjeProf Zvanje { get; set; }

        public int KolikoDoReizbora()
        {
            if (Zvanje.ToString() == "Asistent")
            {
                return 4 - (DateTime.Now.Year - DatumIzbora.Year);
            }
            else
            {
                return 5 - (DateTime.Now.Year - DatumIzbora.Year);
            }
        }
    }
}
