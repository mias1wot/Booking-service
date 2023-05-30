using BookingServiceApp.API.Ride.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Validators.Ride
{
	public class GetAvailableRoutesRequestValidator : AbstractValidator<GetAvailableRoutesRequest>
	{
		public GetAvailableRoutesRequestValidator()
		{
			RuleFor(req => req.From).NotNull().NotEmpty();
			RuleFor(req => req.To).NotNull().NotEmpty();
			RuleFor(req => req.DepartureTime).NotNull().NotEmpty().Must(date => date > DateTime.Now).WithMessage("Departure date is incorrect.");
			RuleFor(req => req.NumberOfSeats).GreaterThan(0);
		}
	}
}
