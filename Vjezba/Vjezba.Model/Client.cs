using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vjezba.Model;

public enum Gender
{
	Male,
	Female
}

public class Client
{
	[Key]
	public int ID { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "First name must contain 2 to 50 characters.")]
	public string FirstName { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must contain 2 to 50 characters.")]
	public string LastName { get; set; }

	[Required]
	[EmailAddress(ErrorMessage = "Invalid E-mail address.")]
	public string Email { get; set; }

	[Range(0, 100, ErrorMessage = "Working experience has to be in range from 0 to 100 years.")]
	public int? WorkingExperience { get; set; }

	public Gender? Gender{ get; set; }

	public string? Address { get; set; }

	[Required]
	public string PhoneNumber { get; set; }

	[ForeignKey(nameof(City))]
	public int? CityID { get; set; }
	public City? City { get; set; }

	public string FullName => $"{FirstName} {LastName}";

	public virtual ICollection<Meeting>? Meetings { get; set; }
}