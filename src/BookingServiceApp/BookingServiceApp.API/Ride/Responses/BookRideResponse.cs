using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Responses
{
	public class BookRideResponse
	{
		public int RideId { get; set; }
		public string RouteId { get; set; }
		public int TicketCode { get; set; }
		public DateTime DepartureTime { get; set; } = new DateTime();
		public DateTime ArrivalTime { get; set; } = new DateTime();
		public string From { get; set; }
		public string To { get; set; }
		public string ExtraInfo { get; set; } = string.Empty;
		public IEnumerable<SeatResponse> Seats { get; set; } = new List<SeatResponse>();
	}

	public class SeatResponse
	{
		public int SeatId { get; set; }
		public int Number { get; set; }
	}
}
