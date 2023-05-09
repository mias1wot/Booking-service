using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Responses
{
	public class GetAvailableRoutesResponse
	{
		public IList<AvailableRoute> Routes { get; set; }
	}

	public class AvailableRoute
	{
		public int Id { get; set; }
		public string RouteId { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public int SeatsAvailable { get; set; }
		public string ExtraInfo { get; set; }
	}
}
