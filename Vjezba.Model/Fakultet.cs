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

	}
}
