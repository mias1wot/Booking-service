using BookingServiceApp.API.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Validators.Auth
{
	public class LoginRequestValidator: AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(req => req.Email).NotNull().NotEmpty();
			RuleFor(req => req.Password).NotNull().NotEmpty().Length(8, 60);
		}
	}
}
