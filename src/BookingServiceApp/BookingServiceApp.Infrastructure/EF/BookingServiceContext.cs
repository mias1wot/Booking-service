﻿using BookingServiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

// Used Microsoft.EntityFrameworkCore version 5.0.17 for .Net Core 3.1

/*
    Add migrations:
Add-migration Initial -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.Infrastructure

For this to work you need to install Microsoft.EntityFrameworkCore.Design (5.0.17) (or Tools which includes Desing) .

*/

/*
    Managing migrations:
Add-migration MigName -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.Infrastructure

Update-database -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.Infrastructure

    Rollback to MigName migration:
Update-Database MigName -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.Infrastructure 


    Remove last migration:
Remove-migration -Project BookingServiceApp.Infrastructure -StartupProject BookingServiceApp.Infrastructure
*/

namespace BookingServiceApp.Infrastructure.EF
{
	public class BookingServiceContext: DbContext
	{
		public BookingServiceContext()
		{
		}

		public BookingServiceContext(DbContextOptions<BookingServiceContext> options) : base(options)
		{
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Route> Routes { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// For this to work you need to install Microsoft.Extensions.Configuration.Json (5.0.0 version for Core 3.1) for ConfigurationBuilder
				// and Microsoft.EntityFrameworkCore.SqlServer (5.0.17) for UseSqlServer extension method
				// Also you need to set 'Copy to output directory' to appsetting.json (through appsetting.json properties)
				IConfigurationRoot configuration = new ConfigurationBuilder()
					.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
					.AddJsonFile("appsettings.json")
					.Build();
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookingService"));
			}
		}
	}
}
