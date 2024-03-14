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

        public IEnumerable<Profesor> DohvatiProfesore()
        {
            List <Profesor> tmpProfs = new List<Profesor>();
            foreach (var item in listOsobe)
            {
                if(item is Profesor p)
                {
                    tmpProfs.Add(p);
                }
            }

            return tmpProfs.OrderBy(p => p.DatumIzbora); 
        }

        public IEnumerable<Student> DohvatiStudente91()
        {
            List <Student> tmpStuds = new List<Student>();
            foreach (var item in listOsobe)
            {
                if (item is Student s) { 
                tmpStuds.Add(s);
                }
            }

            return tmpStuds.Where(s => s.DatumRodjenja.Year > 1991);
        }

        public IEnumerable <Student> DohvatiStudente91NoLinq()
        {
            List <Student> tmpStuds = new List<Student>();
            foreach (var item in listOsobe)
            {
                if (item is Student s) if(item.DatumRodjenja.Year >1991)
                {
                    tmpStuds.Add(s);
                }
            }

            return tmpStuds;
        }


        public IEnumerable<Student> StudentiNeTvzD()
        {
            List<Student> tmpStuds = new List<Student>();
            foreach (var item in listOsobe)
            {
                if (item is Student s)
                {
                    tmpStuds.Add(s);
                }
            }

            return tmpStuds.Where(s => !s.JMBAG.StartsWith("0246") && s.Prezime.StartsWith('D'));
        }

        public List <Student> DohvatiStudente91List() {
            return DohvatiStudente91().ToList();
        }
    }
}
