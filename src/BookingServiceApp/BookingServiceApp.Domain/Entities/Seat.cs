using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingServiceApp.Domain.Entities
{
	public class Seat
	{
		public int SeatId { get; set; }
		public int Number { get; set; }

		[Required]
		public virtual Ride Ride { get; set; }
	}
}
