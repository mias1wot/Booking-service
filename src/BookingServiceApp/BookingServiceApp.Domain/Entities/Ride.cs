using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingServiceApp.Domain.Entities
{
	public class Ride
	{
		[Key]
		public int RideId { get; set; }
		[Required]
		public int RouteId { get; set; }
		[Required]
		public string TicketCode { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		[Required]
		[StringLength(100)]
		public string From { get; set; } = string.Empty;
		[Required]
		[StringLength(100)]
		public string To { get; set; } = string.Empty;
		public string ExtraInfo { get; set; } = string.Empty;

		[Required]
		public virtual User User { get; set; }
		public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
	}
}
