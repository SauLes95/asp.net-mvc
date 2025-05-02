using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Client
    {
        [Key]
		public int ID { get; set; }



		[Required(ErrorMessage = "Unesite ime klijenta.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Unesite prezime klijenta.")] public string LastName { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("City")]
		public int? CityID { get; set; }
        public City City { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
