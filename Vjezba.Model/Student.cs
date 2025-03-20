using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Student : Osoba
	{
		private string JMBAG;
		private int BrPolozeno, ECTS;
		private decimal Prosjek;
		public Student()
		{
		}

		public Student(string _ime, string _prezime, string _OIB, string _JMBG, string _JMBAG, int _brPolozeno, int _ECTS)
			: base(_ime, _prezime, _OIB, _JMBG)
		{
			try
			{
				if (_OIB.Length != 10 || !IsNumeric(_JMBAG))
				{
					throw new InvalidOperationException("Invalid JMBAG");
				}
				else
				{
					this.JMBAG = _JMBAG;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			this.BrPolozeno = _brPolozeno;
			this.ECTS = _ECTS;
			this.Prosjek = _brPolozeno / _ECTS;
		}

		public void SetJMBAG(string _JMBAG)
		{
			try
			{
				if (_JMBAG.Length != 10 || !IsNumeric(_JMBAG))
				{
					throw new InvalidOperationException("Invalid JMBAG");
				}
				else
				{
					this.JMBAG = _JMBAG;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public string GetJMBAG()
		{
			return this.JMBAG;
		}

		public void SetBrPolozeno(int _brPolozeno)
		{
			this.BrPolozeno = _brPolozeno;
		}

		public int GetBrPolozeno()
		{
			return this.BrPolozeno;

		}

		public void SetECTS(int _ECTS)
		{
			this.ECTS = _ECTS;
		}

		public int GetECTS()
		{
			return this.ECTS;
		}

		public decimal GetProsjek()
		{
			return this.Prosjek;
		}

		bool IsNumeric(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < '0' || s[i] > '9')
				{
					return false;
				}
			}
			return true;
		}
	}
}
