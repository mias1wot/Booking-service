using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingServiceApp.Domain.Entities
{
	public class Route
	{
		[Key]
		public int Id { get; set; }
		public Guid RouteGuid { get; set; }
		public int TicketCode { get; set; }
		[StringLength(100)]
		public string From { get; set; } = string.Empty;
		[StringLength(100)]
		public string To { get; set; } = string.Empty;
		public int Seats { get; set; }
		public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		[InverseProperty("Routes")]
		public virtual User User { get; set; }
	}
}
