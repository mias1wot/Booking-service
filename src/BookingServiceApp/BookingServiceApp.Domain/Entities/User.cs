using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingServiceApp.Domain.Entities
{
	public class User
	{
		public int UserId { get; set; }
		[Required]
		[StringLength(100)]
		public string Email { get; set; }
		[Required]
		[StringLength(100)]
		public string Password { get; set; }
		[Required]
		[StringLength(100)]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		[StringLength(100)]
		public string LastName { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }

		public virtual ICollection<Ride> Rides { get; set; } = new HashSet<Ride>();
	}
}
