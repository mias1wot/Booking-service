using BookingServiceApp.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Filters
{
	public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

		public ApiExceptionFilterAttribute()
		{
			_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{typeof(RideConfirmationException), HandleRideConfirmationException},
				{typeof(BookingServiceAppException), HandleBookingServiceAppException},
			};
		}

		public override void OnException(ExceptionContext context)
		{
			HandleException(context);

			base.OnException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			Type type = context.Exception.GetType();
			if(_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext> action))
			{
				action.Invoke(context);

				return;
			}
		}

		private void HandleRideConfirmationException(ExceptionContext context)
		{
			var ex = context.Exception as RideConfirmationException;

			//context.Result = new ObjectResult(string.Join('\n', ex.Errors))
			context.Result = new ObjectResult(ex.Errors)
			{
				StatusCode = ex.StatusCode,
			};

			context.ExceptionHandled = true;
		}

		private void HandleBookingServiceAppException(ExceptionContext context)
		{
			var ex = context.Exception as BookingServiceAppException;

			context.Result = new ObjectResult(ex.Message)
			{
				StatusCode = ex.StatusCode
			};

			context.ExceptionHandled = true;
		}
	}
}
