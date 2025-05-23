namespace Vjezba.Web.Models
{
	public class ClientDTO
	{
		public int ID { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public CityDTO City { get; set; }
	}
}

