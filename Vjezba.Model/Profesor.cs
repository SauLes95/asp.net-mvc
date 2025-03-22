using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public enum Zvanje
	{
		Asistent = 4,
		Predavac = 5,
		VisiPredavac = 5,
		ProfVisokeSkole = 5
	}
	public class Profesor : Osoba
	{

		private string _odjel;
		private DateTime _datumIzbora;
		private Zvanje _zvanje;

		public string Odjel
		{
			get { return this._odjel; }
			set { this._odjel = value; }
		}
		public DateTime DatumIzbora
		{
			get { return this._datumIzbora; }
			set { this._datumIzbora = value; }
		}

		public Zvanje Zvanje
		{
			get { return this._zvanje; }
			set { this._zvanje = value; }
		}


		public Profesor()
		{
		}


		public int KolikoDoReizbora()
		{
			return (int)Zvanje -(DateTime.Now.Year - DatumIzbora.Year);
		}
	}
}
