using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.API.Ride.Requests
{
	public class ValidateTicketRequest
	{
		// User-specific info
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }

		// Route-specific info
		public DateTime DepartureTime { get; set; } = new DateTime();
		public DateTime ArrivalTime { get; set; } = new DateTime();
		public string From { get; set; } = string.Empty;
		public string To { get; set; } = string.Empty;

		public IEnumerable<int> Seats { get; set; } = new List<int>();

		// Encrypted hash
		public string TicketCode { get; set; } = string.Empty;
	}
}
