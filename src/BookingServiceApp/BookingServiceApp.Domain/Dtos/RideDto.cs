using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Domain.Dtos
{
	public class RideDto
	{
        public int RideId { get; set; }
        public string RouteId { get; set; } = string.Empty;
        public int TicketCode { get; set; }
        public DateTime DepartureTime { get; set; } = new DateTime();
        public DateTime ArrivalTime { get; set; } = new DateTime();
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string ExtraInfo { get; set; } = string.Empty;

        public IEnumerable<SeatDto> Seats { get; set; } = new List<SeatDto>();
    }
}
