using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Osoba
	{
		private string _ime, _prezime, _OIB, _JMBG;

		public string Ime
		{
			get { return this._ime; }
			set { this._ime = value; }
		}

		public string Prezime
		{
			get { return this._prezime; }
			set { this._prezime = value; }
		}

		public string OIB
		{
			get { return this._OIB; }
			set 
			{
				if (value.Length != 11 || !IsNumeric(value))
					throw new InvalidOperationException("Invalid OIB");

				this._OIB = value; 
			}
		}

		public string JMBG
		{
			get { return this._JMBG; }
			set 
			{
				if (value.Length != 13 || !IsNumeric(value))
					throw new InvalidOperationException("Invalid JMBG");
				this._JMBG = value; 
			}
		}

		public Osoba()
		{
		}


		public DateTime DatumRodjenja
		{
			get
			{
				int d = int.Parse(JMBG.Substring(0, 2));
				int m = int.Parse(JMBG.Substring(2, 2));
				int y = 1000 + int.Parse(JMBG.Substring(4, 3));

				return new DateTime(y, m, d);
			}
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
