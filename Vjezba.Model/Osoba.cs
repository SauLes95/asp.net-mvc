using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Osoba
	{
		private string Ime, Prezime, OIB, JMBG;
		public Osoba()
		{
		}

		public Osoba(string _ime, string _prezime, string _OIB, string _JMBG)
		{
			this.Ime = _ime;
			this.Prezime = _prezime;

			try
			{
				if (_OIB.Length != 11 || !IsNumeric(_OIB))
				{
					throw new InvalidOperationException("Invalid OIB");
				}
				else
				{
					this.OIB = _OIB;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			try
			{
				if (_JMBG.Length != 13 || !IsNumeric(_JMBG))
				{
					throw new InvalidOperationException("Invalid JMBG");
				}
				else
				{
					this.JMBG = _JMBG;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void SetIme(string _ime)
		{
			this.Ime = _ime;
		}

		public string GetIme()
		{
			return this.Ime;
		}

		public void SetPrezime(string _prezime)
		{
			this.Prezime = _prezime;
		}
		public string GetPrezime()
		{
			return this.Prezime;
		}

		public void SetOIB(string _OIB)
		{
			try
			{
				if (_OIB.Length != 11 || !IsNumeric(_OIB))
				{
					throw new InvalidOperationException("Invalid OIB");
				}
				else
				{
					this.OIB = _OIB;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public string GetOIB()
		{
			return this.OIB;
		}

		public void SetJMBG(string _JMBG)
		{
			try
			{
				if (_JMBG.Length != 13 || !IsNumeric(_JMBG))
				{
					throw new InvalidOperationException("Invalid JMBG");
				}
				else
				{
					this.JMBG = _JMBG;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public string GetJMBG()
		{
			return this.JMBG;
		}

		public DateTime DatumRodjenja()
		{
			DateTime datumRodjenja = new DateTime();

			datumRodjenja.AddDays(int.Parse(this.JMBG.Substring(0, 2)));
			datumRodjenja.AddMonths(int.Parse(this.JMBG.Substring(2, 2)));
			datumRodjenja.AddYears(1000 + int.Parse(this.JMBG.Substring(4, 3)));

			return datumRodjenja;
		}
		bool IsNumeric(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < '0' || s[i] > '9'){
					return false;
				}
			}
			return true;
		}

	}
}
