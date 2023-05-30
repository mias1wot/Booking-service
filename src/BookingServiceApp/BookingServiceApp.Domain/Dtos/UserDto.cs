using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Domain.Dtos
{
	public class UserDto
	{
		public int UserId { get; set; }
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }
		public IEnumerable<RideDto> Rides { get; set; } = new List<RideDto>();
	}
}
