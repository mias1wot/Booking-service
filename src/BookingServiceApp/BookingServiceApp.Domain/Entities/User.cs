using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingServiceApp.Domain.Entities
{
	public class User
	{
		public User()
		{
			Routes = new HashSet<Route>();
		}

		[Key]
		public int Id { get; set; }
		[StringLength(100)]
		public string FirstName { get; set; } = string.Empty;
		[StringLength(100)]
		public string LastName { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }

		[InverseProperty("User")]
		public ICollection<Route> Routes { get; set; }
	}
}
