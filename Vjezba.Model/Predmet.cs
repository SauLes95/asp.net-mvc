using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Predmet
	{
		private int _sifra, _ECTS;
		private string _naziv;

		public int Sifra
		{
			get { return this._sifra; }
			set { this._sifra = value; }
		}

		public int ECTS
		{
			get { return this._ECTS; }
			set { this._ECTS = value; }
		}

		public string Naziv
		{
			get { return this._naziv; }
			set { this._naziv = value; }
		}

		public Predmet()
		{
		}


	}
}
