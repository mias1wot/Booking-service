using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Application.Exceptions
{
	public class BookingServiceAppException: Exception
	{
		public int StatusCode { get; set; }

		public BookingServiceAppException(string message, int code): base(message)
		{
			StatusCode = code;
		}

		public BookingServiceAppException(string message, Exception innerException, int code) : base(message, innerException)
		{
			StatusCode = code;
		}
	}
}
