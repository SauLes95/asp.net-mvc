using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Student : Osoba
	{
		private string _JMBAG;
		private int _brPolozeno;
		private decimal _prosjek;


		public string JMBAG
		{
			get { return this._JMBAG; }
			set
			{

				if (value.Length != 10 || !IsNumeric(value))
					throw new InvalidOperationException("Invalid JMBAG");

				this._JMBAG = value;
			}
		}

		public int BrPolozeno
		{
			get { return this._brPolozeno; }
			set { this._brPolozeno = value; }
		}

		private int _ECTS;
		public int ECTS
		{
			get { return this._ECTS; }
			set { this._ECTS = value; }
		}

		public decimal Prosjek
		{
			get { return this._prosjek; }
			set { this._prosjek = value; }
		}

		public Student()
		{
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
