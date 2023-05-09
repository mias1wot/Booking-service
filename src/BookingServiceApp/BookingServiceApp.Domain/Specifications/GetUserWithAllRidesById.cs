using Ardalis.Specification;
using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.Domain.Specifications
{
	public class GetUserWithAllRidesById: Specification<User>
	{
		public GetUserWithAllRidesById(int userId)
		{
			Query.Where(user => user.UserId == userId).Include(user => user.Rides).ThenInclude(ride => ride.Seats);
		}
	}
}
