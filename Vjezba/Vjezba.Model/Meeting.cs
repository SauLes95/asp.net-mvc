using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
	public enum MeetingType
	{
		InPerson,
		VideoCall
	}

	public enum MeetingStatus
	{
		Scheduled,
		Canceled
	}

	public class Meeting
	{
		[Key]
		public int ID { get; set; }
		public MeetingType Type { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public MeetingStatus Status { get; set; }
		public string? Location { get; set; }
		public string? Comments { get; set; }
		[ForeignKey(nameof(ClientID))]
		public int ClientID{ get; set; }
	}
}
