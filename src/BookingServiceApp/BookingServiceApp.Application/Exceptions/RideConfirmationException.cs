using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingServiceApp.Application.Exceptions
{
	public class RideConfirmationException: BookingServiceAppException
	{
		public IEnumerable<string> Errors;
		public RideConfirmationException() : base("The Ride was not booked.", (int)HttpStatusCode.BadRequest)
		{
		}

		public RideConfirmationException(string message) : base(message, (int)HttpStatusCode.BadRequest)
		{
		}

		public RideConfirmationException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.BadRequest)
		{
		}

		public RideConfirmationException(IEnumerable<string> errors) : base("The Ride was not booked.", (int)HttpStatusCode.BadRequest)
		{
			Errors = errors;
		}
	}
}
