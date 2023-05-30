using BookingServiceApp.API.Ride.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Validators.Ride
{
	public class BookRideRequestValidator : AbstractValidator<BookRideRequest>
	{
		public BookRideRequestValidator()
		{
			RuleFor(req => req.From).NotNull().NotEmpty();
			RuleFor(req => req.To).NotNull().NotEmpty();
			RuleFor(req => req.Seats).NotNull().NotEmpty();
			RuleForEach(req => req.Seats).GreaterThan(0);
		}
	}
}
