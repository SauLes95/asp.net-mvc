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

        public Student NajboljiProsjek(int god)
        {
            List<Student> tmpStuds = new List<Student>();
            foreach (var item in listOsobe)
            {
                if (item is Student s)
                {
                    tmpStuds.Add(s);
                }
            }

            return tmpStuds.Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek).FirstOrDefault();
        }

        public IEnumerable<Student> StudentiGodinaOrdered(int god)
        {
            List<Student> tmpStuds = new List<Student>();
            foreach (var item in listOsobe)
            {
                if (item is Student s)
                {
                    tmpStuds.Add(s);
                }
            }

            return tmpStuds.Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek);
        }

        public IEnumerable<Profesor> SviProfesori(bool asc)
        {
            List<Profesor> tmpProfs = new List<Profesor>();
            foreach (var item in listOsobe)
            {
                if (item is Profesor p)
                {
                    tmpProfs.Add(p);
                }
            }

            if (asc)
            {
                return tmpProfs.OrderBy(p => p.Prezime).ThenBy(p => p.Ime);
            }
            else
            {
                return tmpProfs.OrderByDescending(p => p.Prezime).ThenByDescending(p => p.Ime);
            }
        }

        public int KolikoProfesoraUZvanju(Zvanje zvanje)
        {
            List<Profesor> tmpProfs = new List<Profesor>();
            foreach (var item in listOsobe)
            {
                if (item is Profesor p)
                {
                    tmpProfs.Add(p);
                }
            }

            return tmpProfs.Count(p => p.Zvanje == zvanje);
        }

        public IEnumerable <Profesor> NeaktivniProfesori(int x)
        {
            List<Profesor> tmpProfs = new List<Profesor>();
            foreach (var item in listOsobe)
            {
                if (item is Profesor p)
                {
                    tmpProfs.Add(p);
                }
            }

            return tmpProfs.Where(p => p.Predmeti.Count() < x  && (p.Zvanje.Equals(Zvanje.Predavac) || p.Zvanje.Equals(Zvanje.VisiPredavac)));
        }
    }
}
