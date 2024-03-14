using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Osoba
    {

        public Osoba() { }

        string number = @"^[0-9]+$";
        public string Ime { get; set; }
        public string Prezime { get; set; }
        private string oib;
        private string jmbg;
        public string OIB
        {
            get { return this.oib;}
            set
            {
                if(value.Length != 11 || !Regex.IsMatch(value, number)){
                    throw new InvalidOperationException("OIB mora sadrzavati 11 brojeva");
                }
                else
                {
                    this.oib = value;
                }
            }
        }

        public string JMBG
        {
            get { return this.jmbg;}
            set
            {
                if (value.Length != 13 || !Regex.IsMatch(value, number)){
                    throw new InvalidOperationException("JMBG mora sadrzavati 11 brojeva");
                }
                else
                {
                    this.jmbg = value;
                }
            }
        }

        public DateTime DatumRodjenja
        {
            get
            {
                int tmpDay = Convert.ToInt32(jmbg.Substring(0, 2));
                int tmpMonth = Convert.ToInt32(jmbg.Substring(2, 2));
                int tmpYear = 1000 + Convert.ToInt32(jmbg.Substring(4, 3));

                return new DateTime(tmpYear,tmpMonth,tmpDay);
            }
        }

    }
}
