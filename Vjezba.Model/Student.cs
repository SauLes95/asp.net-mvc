using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vjezba.Model
{
    public class Student : Osoba
    {
        private string number = @"^[0-9]+$";
        public Student() { }

        public string jmbag;
        public string JMBAG
        {
            get { return this.jmbag; }
            set
            {
                if (value.Length != 10 || !Regex.IsMatch(value, number))
                {
                    throw new InvalidOperationException("JMBAG mora sadrzavati 10 brojeva");
                }
            }
        }

        public decimal Prosjek { get; set; }
        public int BrPolozeno { get; set; }
        public int ECTS { get; set; }
    }
}
