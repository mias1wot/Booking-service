using AutoMapper;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.AutoMapperProfiles
{
	public class ApplicationProfile : Profile
	{
		public ApplicationProfile()
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
			CreateMap<User, UserDto>()
				.ReverseMap().ForPath(user => user.Rides, opt => opt.Ignore());
			CreateMap<Ride, RideDto>();
			CreateMap<Ride, RideConfirmationDto>();
			CreateMap<Seat, SeatDto>().ReverseMap();


			// Dto => Dto
			CreateMap<RideConfirmationDto, Ride>();
		}
	}
}
