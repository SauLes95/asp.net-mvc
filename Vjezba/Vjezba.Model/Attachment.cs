using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public class Attachment
	{
		[Key]
		public int ID { get; set; }

		public required string FileName { get; set; }
		public required string FilePath { get; set; }

		[ForeignKey(nameof(Client))]
		public int ClientID { get; set; }
		public required Client Client { get; set; }
	}
}
