using Axess.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Axess.Infrastructure.Contexts;

/// <summary>
/// 
/// </summary>
public class ApplicationDbContextInitialiser
{
	private readonly ILogger<ApplicationDbContextInitialiser> _logger;
	private readonly ApplicationDbContext _context;

	public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
	{
		_logger = logger;
		_context = context;
	}

	public async Task MigrateDatabaseAndSeedAsync()
	{
		_logger.LogInformation("MigrateDatabaseAndSeedAsync started");
		try
		{
			if (_context.Database.IsNpgsql())
			{
				var retryPolicy = Policy.Handle<Exception>()
					.WaitAndRetry(
						retryCount: 5,
						// 2 secs, 4, 8, 16, 32 
						sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
						onRetry: (exception, retryCount, context) =>
						{
							_logger.LogError("Retrying MigrateDatabaseAndSeed {RetryCount} of {ContextPolicyKey} at {ContextOperationKey}, due to: {Exception}", retryCount, context.PolicyKey,
								context.OperationKey, exception);
						});

				_ = retryPolicy.Execute(MigrateAndSeedAsync);
			}
			else if (_context.Database.IsInMemory())
			{

				await SeedDatabaseAsync();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database");
			throw;
		}

		_logger.LogInformation("MigrateDatabaseAndSeedAsync completed");
	}

	private async Task MigrateAndSeedAsync()
	{
		_context.Database.Migrate();
		await SeedDatabaseAsync();
	}
	/// <summary>
	/// 
	/// </summary>
	public async Task SeedDatabaseAsync()
	{

		_context.Addresses.Add(new Address(Guid.NewGuid())
		{
			Street = "1 Microsoft Way",
			City = "Redmond",
			State = "WA",
			ZipCode = "98052"
		});
		var rnd = Random.Shared;
		var orders =
			Enumerable
				.Range(1, 20)
				.Select(index =>
				{
					Faker faker = new();
					var person = faker.Person;

					Order order = new(Guid.NewGuid(), $"{person.FullName}")
					{
						EffectiveDate = DateTime.UtcNow.AddDays(faker.Random.Double(0, 15)),
					};


					var elements = faker.Random.Int(0, 5);
					for (var i = 0; i < elements; i++)
					{
						LineItem lineItem = new(Guid.NewGuid())
						{
							Description = faker.Lorem.Slug(rnd.Next(3, 5)),
							Fulfilled = faker.Random.Bool(),
							Quantity = faker.Random.Int(1, 50),
							UnitPrice = faker.Finance.Amount(min: 10, max: 9999, decimals: 2),
							OrderId = order.Id,
						};
						order.AddLineItem(lineItem);
					}
					return order;
				});

		await _context.Orders.AddRangeAsync(orders);
		await _context.SaveChangesAsync();
	}
}
