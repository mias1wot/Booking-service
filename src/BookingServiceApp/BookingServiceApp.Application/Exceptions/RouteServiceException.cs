using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingServiceApp.Application.Exceptions
{
	public class RouteServiceException : BookingServiceAppException
	{
		public RouteServiceException() : base("Error occured while calling RouteService.", (int)HttpStatusCode.InternalServerError)
		{
		}

		public RouteServiceException(string message) : base(message, (int)HttpStatusCode.InternalServerError)
		{
		}

		public RouteServiceException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.InternalServerError)
		{
		}

		public RouteServiceException(string message, HttpStatusCode httpStatusCode) : base(message, (int)httpStatusCode)
		{
		}
	}
}
