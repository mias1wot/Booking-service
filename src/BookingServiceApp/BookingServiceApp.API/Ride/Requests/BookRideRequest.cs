using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Requests
{
	public class BookRideRequest
	{
		public string RouteId { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public DateTime DepartureTime { get; set; }
		public int NumberOfSeats { get; set; }
	}
}
