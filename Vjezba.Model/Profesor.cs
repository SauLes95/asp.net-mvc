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

		private string Odjel;
		private DateTime DatumIzbora;
		private Zvanje Zvanje;
		public Profesor()
		{
		}

		public Profesor(string _ime, string _prezime, string _OIB, string _JMBG, string _odjel, Zvanje _zvanje, DateTime DatumIzbora)
			: base(_ime, _prezime, _OIB, _JMBG)
		{
			this.Odjel = _odjel;
			this.Zvanje = _zvanje;
			this.DatumIzbora = DatumIzbora;
		}

		public void setOdjel(string _odjel)
		{
			this.Odjel = _odjel;
		}

		public string getOdjel()
		{
			return this.Odjel;
		}

		public void setZvanje(Zvanje _zvanje)
		{
			this.Zvanje = _zvanje;
		}

		public Zvanje getZvanje()
		{
			return this.Zvanje;
		}

		public void setDatumIzbora(DateTime _datumIzbora)
		{
			this.DatumIzbora = _datumIzbora;
		}

		public DateTime getDatumIzbora()
		{
			return this.DatumIzbora;
		}

		public int KolikoDoReizbora()
		{
			return (int)Zvanje -(DateTime.Now.Year - DatumIzbora.Year);
		}
	}
}
