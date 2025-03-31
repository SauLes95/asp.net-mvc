using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Fakultet
	{

		public List<Osoba> Osobe { get; set; }

		public Fakultet()
		{
			Osobe = new List<Osoba>();
		}

		public int KolikoProfesora()
		{
			int tmpProfCounter = 0;
			foreach(Osoba osoba in Osobe)
			{
				if (osoba is Profesor)
				{
					tmpProfCounter++;
				}
			}

			return tmpProfCounter;
		}

		public int KolikoStudenata()
		{
			int tmpStudCounter = 0;
			foreach (Osoba osoba in Osobe)
			{
				if (osoba is Student)
				{
					tmpStudCounter++;
				}
			}

			return tmpStudCounter;
		}

		public Student DohvatiStudenta(string _JMBAG)
		{
			foreach (Osoba osoba in Osobe)
			{
				if (osoba is Student)
				{
					Student tmpStudent = (Student)osoba;
					if (tmpStudent.JMBAG == _JMBAG)
					{
						return tmpStudent;
					}
				}
			}
			return null;
		}

		public IEnumerable<Profesor> DohvatiProfesore()
		{
			List<Profesor> tmpProfesori = new List<Profesor>();
			foreach (Osoba osoba in Osobe)
			{
				if (osoba is Profesor)
				{
					tmpProfesori.Add((Profesor)osoba);
				}
			}


			return tmpProfesori.OrderBy(tmpProfesori => tmpProfesori.DatumIzbora);
		}

		public IEnumerable<Student> DohvatiStudente91()
		{
			return Osobe.OfType<Student>()
				.Where(t => t.DatumRodjenja.Year > 1991);
		}

		public IEnumerable<Student> DohvatiStudente91NoLinq()
		{
			List<Student> tmpStudenti = new List<Student>();
			foreach (Osoba osoba in Osobe)
			{
				if (osoba is Student && osoba.DatumRodjenja.Year > 1991)
				{
					tmpStudenti.Add((Student)osoba);
				}
			}
			return tmpStudenti;
		}

		public IEnumerable<Student> StudentiNeTvzD()
		{
			return Osobe.OfType<Student>()
				.Where(s => s.Prezime.StartsWith('D'))
				.Where(s => !s.JMBAG.StartsWith("0246"));
		}

		public IEnumerable<Student> DohvatiStudente91List()
		{
			return Osobe.OfType<Student>().Where(s => s.DatumRodjenja.Year > 1991).ToList();
		}

		public Student? NajboljiProsjek(int god)
		{
			return Osobe.OfType<Student>()
				.Where(s => s.DatumRodjenja.Year == god)
				.OrderByDescending(s => s.Prosjek)
				.FirstOrDefault();
		}

		public IEnumerable<Student> StudentiGodinaOrdered(int god)
		{
			return Osobe.OfType<Student>()
				.Where(s => s.DatumRodjenja.Year == god)
				.OrderByDescending(s => s.Prosjek);
		}

		public IEnumerable<Profesor> SviProfesori(bool asc)
		{
			if (asc)
			{
				return Osobe.OfType<Profesor>()
					.OrderBy(p => p.Ime)
					.OrderBy(p => p.Prezime);
			}
			else
			{
				return Osobe.OfType<Profesor>()
					.OrderByDescending(p => p.Prezime)
					.OrderByDescending(p => p.Ime);
			}
		}
	}
}
