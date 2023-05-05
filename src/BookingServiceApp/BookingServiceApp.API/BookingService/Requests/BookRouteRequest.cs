using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.BookingService.Requests
{
	public class BookRouteRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		public int Seats { get; set; }
	}
}
