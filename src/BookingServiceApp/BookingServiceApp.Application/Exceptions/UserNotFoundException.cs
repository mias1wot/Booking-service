using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingServiceApp.Application.Exceptions
{
	public class UserNotFoundException: BookingServiceAppException
	{
		public UserNotFoundException() : base("User not found.", (int)HttpStatusCode.NotFound)
		{
		}

		public UserNotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
		{
		}

		public UserNotFoundException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.NotFound)
		{
		}

		public UserNotFoundException(int userId) : base($"User with id {userId} not found.", (int)HttpStatusCode.NotFound)
		{
		}
	}
}
