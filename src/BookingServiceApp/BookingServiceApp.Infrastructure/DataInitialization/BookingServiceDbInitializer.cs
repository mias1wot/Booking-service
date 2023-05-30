using BookingServiceApp.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RouteServiceAPP.Infrastructure.DataInitialization
{
	public static class BookingServiceDbInitializer
	{
		public static void InitializeData(BookingServiceContext context)
		{
		}

		public static void ClearData(BookingServiceContext context, bool deleteOnlyRides)
		{
			ExecuteDeleteSql(context, nameof(BookingServiceContext.Seats));
			ExecuteDeleteSql(context, nameof(BookingServiceContext.Rides));

			if (!deleteOnlyRides)
			{
				ExecuteDeleteSql(context, nameof(BookingServiceContext.Users));
			}

			ResetIdentity(context, deleteOnlyRides);
		}
		private static void ExecuteDeleteSql(BookingServiceContext context, string tableName)
		{
			var rawSqlString = $"Delete from dbo.{tableName}";
			context.Database.ExecuteSqlRaw(rawSqlString);
		}
		private static void ResetIdentity(BookingServiceContext context, bool deleteOnlyRides)
		{
			var tables = new List<string> { nameof(BookingServiceContext.Seats), nameof(BookingServiceContext.Rides) };

			if (!deleteOnlyRides)
			{
				tables.Add(nameof(BookingServiceContext.Users));
			}


			foreach (var table in tables)
			{
				var rawSqlString = $"DBCC CHECKIDENT (\"dbo.{table}\", RESEED, 0);";
				context.Database.ExecuteSqlRaw(rawSqlString);
			}
		}

		public static void RecreateDatabase(BookingServiceContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.Migrate();
		}
	}


	public static class DbSetExtensions
	{
		public static void AddOrUpdate<T>(this DbSet<T> dbSet, Expression<Func<T, object>> identifierExpression, IEnumerable<T> entities)
			where T : class
		{
			var identifierFunc = identifierExpression.Compile();
			var existingEntities = dbSet.ToDictionary(entity => identifierFunc(entity));

			foreach (var entity in entities)
			{
				var identifier = identifierFunc(entity);
				if (existingEntities.TryGetValue(identifier, out var existingEntity))
				{
					dbSet.Update(existingEntity);

					var dbContext = dbSet.GetService<BookingServiceContext>();
					// Copy the property values from the input entity to the existing entity
					dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
				}
				else
				{
					dbSet.Add(entity);
				}
			}
		}
	}
}
