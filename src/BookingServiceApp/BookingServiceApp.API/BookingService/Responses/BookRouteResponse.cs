using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.BookingService.Responses
{
	public class BookRouteResponse
	{
		public int Id { get; set; }
		public Guid RouteGuid { get; set; }
		public int TicketCode { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public int Seats { get; set; }
	}
}
