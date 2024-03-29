﻿using BookingServiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;



/*
	For migrations to work you need to install:
	API:
		Microsoft.EntityFrameworkCore.Tools (5.0.17 for .Net Core 3.1)
	Infrastructure:
		Microsoft.EntityFrameworkCore.Tools (5.0.17)
		Microsoft.EntityFrameworkCore version (5.0.17)


    Add migration:
Add-migration -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.API MigName

Update-database -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.API

    Rollback to MigName migration:
Update-Database -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.API MigName

    Remove last migration:
Remove-migration -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.API
*/


namespace BookingServiceApp.Infrastructure.EF
{
	public class BookingServiceContext: DbContext
	{
		private readonly IConfiguration _configuration;
		public BookingServiceContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public BookingServiceContext(DbContextOptions<BookingServiceContext> options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Ride> Rides { get; set; }
		public virtual DbSet<Seat> Seats { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// For this to work you need to install Microsoft.Extensions.Configuration.Json (5.0.0 version for Core 3.1) for ConfigurationBuilder
				// and Microsoft.EntityFrameworkCore.SqlServer (5.0.17) for UseSqlServer extension method
				// Also you need to set 'Copy to output directory' to appsetting.json (through appsetting.json properties)
				//IConfigurationRoot configuration = new ConfigurationBuilder()
				//	.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				//	.AddJsonFile("appsettings.json")
				//	.Build();
				//optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookingService"));

				optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BookingService"));
			}
		}
	}
}
