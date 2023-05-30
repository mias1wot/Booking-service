using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Domain.Dtos
{
	public class BookRideParamsDto
	{
		public int RouteId { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public IEnumerable<int> Seats { get; set; }
	}
}
