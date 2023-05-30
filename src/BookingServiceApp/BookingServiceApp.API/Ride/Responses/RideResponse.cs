using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Ride.Responses
{
    public class RideResponse
	{
        public int RideId { get; set; }
        public int RouteId { get; set; }
        public string TicketCode { get; set; }
        public DateTime DepartureTime { get; set; } = new DateTime();
        public DateTime ArrivalTime { get; set; } = new DateTime();
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string ExtraInfo { get; set; } = string.Empty;

        public IEnumerable<SeatResponse> Seats { get; set; } = new List<SeatResponse>();
    }
}
