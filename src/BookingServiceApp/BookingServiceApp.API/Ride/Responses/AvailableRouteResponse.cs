using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Responses
{
	public class AvailableRouteResponse
	{
		public int RouteId { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public IEnumerable<int> SeatsAvailable { get; set; }
		public string ExtraInfo { get; set; }
	}
}
