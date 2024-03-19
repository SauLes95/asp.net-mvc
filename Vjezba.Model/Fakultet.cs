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
            return FilterOsobeByType(listOsobe,typeof(Profesor)).Count();
        }

        public int KolikoStudenata()
        {
            return FilterOsobeByType(listOsobe, typeof(Student)).Count();
        }

        public Student DohvatiStudenta(string JMBAG)
        {
            return FilterOsobeByType(listOsobe, typeof(Student)).Cast<Student>()
                .Where(s => s.jmbag == JMBAG).FirstOrDefault();
        }

        public IEnumerable<Profesor> DohvatiProfesore()
        {
            return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                .OrderBy(p => p.DatumIzbora); 
        }

        public IEnumerable<Student> DohvatiStudente91()
        {
            return FilterOsobeByType(listOsobe, typeof(Student)).Cast<Student>()
                .Where(s => s.DatumRodjenja.Year > 1991);
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
            return FilterOsobeByType(listOsobe, typeof(Student)).Cast<Student>().ToList()
                .Where(s => !s.JMBAG.StartsWith("0246") && s.Prezime.StartsWith('D'));
        }

        public List <Student> DohvatiStudente91List() {
            return DohvatiStudente91().ToList();
        }

        public Student NajboljiProsjek(int god)
        {
            return FilterOsobeByType(listOsobe, typeof(Student)).Cast<Student>().ToList()
                .Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek).FirstOrDefault();
        }

        public IEnumerable<Student> StudentiGodinaOrdered(int god)
        {
            return FilterOsobeByType(listOsobe, typeof(Student)).Cast<Student>().ToList()
                .Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek);
        }

        public IEnumerable<Profesor> SviProfesori(bool asc)
        {
            if (asc)
            {
                return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                    .OrderBy(p => p.Prezime).ThenBy(p => p.Ime);
            }
            else
            {
                return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                    .OrderByDescending(p => p.Prezime).ThenByDescending(p => p.Ime);
            }
        }

        public int KolikoProfesoraUZvanju(Zvanje zvanje)
        {
            return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                .Count(p => p.Zvanje == zvanje);
        }

        public IEnumerable <Profesor> NeaktivniProfesori(int x)
        {
            return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                .Where(p => p.Predmeti.Count() < x  && (p.Zvanje.Equals(Zvanje.Predavac) || p.Zvanje.Equals(Zvanje.VisiPredavac)));
        }

        public IEnumerable<Profesor> AktivniAsistenti(int x, int minEcts)
        {
            return FilterOsobeByType(listOsobe, typeof(Profesor)).Cast<Profesor>().ToList()
                .Where(p => p.Predmeti.Count() > x && p.Predmeti.Any(pr => pr.ECTS >= minEcts));
        }

        public void IzmjeniProfesore(Action<Profesor> action)
        {
            List<Profesor> tmpProfs = new List<Profesor>();
            foreach (var item in listOsobe)
            {
                if (item is Profesor p)
                {
                    action(p);
                    tmpProfs.Add(p);
                }
            }
        }

        private IEnumerable<Osoba> FilterOsobeByType(List <Osoba> listOsobe, Type t)
        {
            List<Osoba> filtriraneOsobe = new List<Osoba>();

            foreach (var osoba in listOsobe)
            {
                if ((t == typeof(Profesor) && osoba is Profesor) || (t == typeof(Student) && osoba is Student))
                {
                    filtriraneOsobe.Add(osoba);
                }
            }

            return filtriraneOsobe;
        }
    }
}
