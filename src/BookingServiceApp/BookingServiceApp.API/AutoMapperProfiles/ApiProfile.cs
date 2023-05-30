using AutoMapper;
using BookingServiceApp.API.BookingService.Responses;
using BookingServiceApp.API.Ride.Requests;
using BookingServiceApp.API.Ride.Responses;
using BookingServiceApp.API.User.Requests;
using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.AutoMapperProfiles
{
	public class ApiProfile : Profile
	{
		public ApiProfile()
		{
			/* Examples:
			CreateMap<User, ClientDTO>();
			CreateMap<UserForRegistrationDto, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
				.ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Name));


			CreateMap<Ride, RideDto>().ForMember(x => x.Orders, opt => opt.Ignore());
			CreateMap<User, UserDto>().ForMember(userDto => userDto.Rides, opt => opt.MapFrom(ride => ride.Rides));
			*/


			// Request => Dto
			CreateMap<CreateUserRequest, UserDto>();
			CreateMap<UpdateUserRequest, UserDto>();
			CreateMap<GetAvailableRoutesRequest, RouteSearchParamsDto>();
			CreateMap<BookRideRequest, BookRideParamsDto>();


			// Dto => Response
			CreateMap<UserDto, UserResponse>();
			CreateMap<RouteDto, AvailableRouteResponse>();
			CreateMap<RideDto, BookRideResponse>();
			CreateMap<RideDto, RideResponse>();

			CreateMap<SeatDto, SeatResponse>();
		}
	}
}
