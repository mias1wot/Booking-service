using AutoMapper;
using BookingServiceApp.API.BookingService.Responses;
using BookingServiceApp.API.Ride.Requests;
using BookingServiceApp.API.Ride.Responses;
using BookingServiceApp.API.User.Requests;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.AutoMapperProfiles
{
	public class InventoryProfile : Profile
	{
		public InventoryProfile()
		{
			/* Examples:
			CreateMap<User, ClientDTO>();
			CreateMap<UserForRegistrationDto, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
				.ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Name));


			CreateMap<Ride, RideDto>().ForMember(x => x.Orders, opt => opt.Ignore());
			CreateMap<User, UserDto>().ForMember(userDto => userDto.Rides, opt => opt.MapFrom(ride => ride.Rides));
			*/

			// Model => Dto & Dto => Model
			CreateMap<Domain.Entities.User, UserDto>().AfterMap((user, userDto, context) => userDto.Rides = context.Mapper.Map<List<RideDto>>(user.Rides))
				.ReverseMap().ForPath(user => user.Rides, opt => opt.Ignore());
			CreateMap<Domain.Entities.Ride, RideDto>().AfterMap((ride, rideDto, context) => rideDto.Seats = context.Mapper.Map<List<SeatDto>>(ride.Seats));
			CreateMap<Domain.Entities.Ride, RideConfirmationDto>().AfterMap((ride, rideDto, context) => rideDto.Seats = context.Mapper.Map<List<SeatDto>>(ride.Seats)); ;
			CreateMap<Seat, SeatDto>();


			// Request => Dto
			CreateMap<CreateUserRequest, UserDto>();
			CreateMap<UpdateUserRequest, UserDto>();
			CreateMap<GetAvailableRoutesRequest, RouteSearchParamsDto>();
			CreateMap<BookRideRequest, BookRideParamsDto>();


			// Dto => Response
			CreateMap<UserDto, UserResponse>();
			CreateMap<RouteDto, AvailableRoute>();
			CreateMap<RideDto, BookRideResponse>().AfterMap((rideDto, response, context) => response.Seats = context.Mapper.Map<List<SeatResponse>>(rideDto.Seats));
			CreateMap<RideDto, RideResponse>().AfterMap((rideDto, response, context) => response.Seats = context.Mapper.Map<List<SeatResponse>>(rideDto.Seats));


			// Dto => Dto
			CreateMap<RideConfirmationDto, Domain.Entities.Ride>().
				AfterMap((rideConfirmationDto, ride, context) => ride.Seats = context.Mapper.Map<List<Seat>>(rideConfirmationDto.Seats));
		}
	}
}
