using Ardalis.Specification;
using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Domain.Specifications
{
	public class GetUserByEmailAndPassword: Specification<User>
	{
		public GetUserByEmailAndPassword(string email, string password)
		{
			Query.Where(user => user.Email == email && user.Password == password);
		}
	}
}
