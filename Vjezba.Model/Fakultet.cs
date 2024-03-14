using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Fakultet
    {
        public List<Osoba> listOsobe { get; set; }

        public Fakultet() { 
            listOsobe = new List<Osoba>();
        }
        
        public int KolikoProfesora()
        {
            int profCounter = 0;
            foreach (var item in listOsobe)
            {
                if(item is Profesor)
                {
                    profCounter++;
                }
            }
            return profCounter;
        }

        public int KolikoStudenata()
        {
            int studCounter = 0;
            foreach (var item in listOsobe)
            {
                if (item is Student)
                {
                    studCounter++;
                }
            }
            return studCounter;
        }

        public Student DohvatiStudenta(string JMBAG)
        {
            foreach(var item in listOsobe)
            {
                if(item is Student tmpStudent) if (tmpStudent.jmbag == JMBAG) { 
                    
                    return tmpStudent;
                }  
            }

            return null;
        }
    }
}
