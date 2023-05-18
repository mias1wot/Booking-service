using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Requests
{
	public class BookRideRequest
	{
		public int RouteId { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public IEnumerable<int> Seats { get; set; }
	}
}
