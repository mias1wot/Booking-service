using BookingServiceApp.API.User.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Validators.User
{
	//FluentValidation.AspNetCore (v. 9.2)
	public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
	{
		public CreateUserRequestValidator()
		{
			RuleFor(req => req.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(req => req.Password).NotNull().NotEmpty().Length(8, 60);
			RuleFor(req => req.FirstName).NotNull().NotEmpty().MaximumLength(100);
			RuleFor(req => req.LastName).NotNull().NotEmpty().MaximumLength(100);
			RuleFor(req => req.BirthDate).NotNull().NotEmpty().Must(birthDate =>
			{
				return birthDate >= new DateTime(1900, 1, 1) && birthDate <= DateTime.Now;
			}).WithMessage("Birth date is incorrect.");
		}
	}
}
