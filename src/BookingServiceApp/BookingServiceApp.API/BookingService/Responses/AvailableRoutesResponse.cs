using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.BookingService.Responses
{
	public class AvailableRoutesResponse
	{
		public IList<AvailableRoute> Routes { get; set; }
	}

	public class AvailableRoute
	{
		public int Id { get; set; }
		public Guid RouteGuid { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public int AvailableSeats { get; set; }
		public string Driver { get; set; }
		public string Bus { get; set; }
	}
}
