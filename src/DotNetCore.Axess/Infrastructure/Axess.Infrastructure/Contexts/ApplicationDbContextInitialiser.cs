using Axess.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;

/// <summary>
/// 
/// </summary>
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    // private readonly UserManager<ApplicationUser> _userManager;
    // private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context) //, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        // _userManager = userManager;
        // _roleManager = roleManager;
    }

    public void MigrateDatabaseAndSeed()
    {
        _logger.LogInformation("MigrateDatabaseAndSeedAsync started");
        try
        {
            if (_context.Database.IsNpgsql())
            {
                Polly.Retry.RetryPolicy retryPolicy = Policy.Handle<Exception>()
                    .WaitAndRetry(
                        retryCount: 5,
                        // 2 secs, 4, 8, 16, 32 
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, retryCount, context) =>
                        {
                            _logger.LogError("Retrying MigrateDatabaseAndSeed {RetryCount} of {ContextPolicyKey} at {ContextOperationKey}, due to: {Exception}", retryCount, context.PolicyKey,
                                context.OperationKey, exception);
                        });

                retryPolicy.Execute(MigrateAndSeed);
            }
            else if (_context.Database.IsInMemory())
            {

                SeedDatabase();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }

        _logger.LogInformation("MigrateDatabaseAndSeedAsync completed");
    }

    private void MigrateAndSeed()
    {
        _context.Database.Migrate();
        SeedDatabase();
    }
    /// <summary>
    /// 
    /// </summary>
    public void SeedDatabase()
    {

        _context.Addresses.Add(new Address(Guid.NewGuid())
        {
            Street = "1 Microsoft Way",
            City = "Redmond",
            State = "WA",
            ZipCode = "98052"
        });
        /*
         Order order1 = new Order(1) { Customer = "John Doe" };
         order1.AddLineItem(new LineItem(1) { Description = "Description", Number = 5, Fulfilled = false, Quantity = 23, UnitPrice = decimal.One });
        */


        Random rnd = Random.Shared;



        IEnumerable<Order> orders =
            Enumerable
                .Range(1, 20)
                .Select(index =>
                {
                    Faker faker = new();
                    Bogus.Person person = faker.Person;

                    Order order = new(Guid.NewGuid())
                    {
                        Customer = $"{person.FullName}",
                        EffectiveDate = DateTime.UtcNow.AddDays(faker.Random.Double(0, 15)),
                    };


                    int elements = faker.Random.Int(0, 5);
                    for (int i = 0; i < elements; i++)
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

        int countOrders = orders.Count();

        _context.Orders.AddRange(orders);
        //_context.LineItems.AddRange(lineItems);

        /*_context.Orders.ForEachAsync(order =>
        {

            _context.LineItems.ForEachAsync(item =>
            {
                order.LineItems.Add(item);
            });

        });*/

        /*
        _context.Orders.AddRange(new []
        {
            order1,
            new Order(2){ Customer = "John Doe" },
            new Order(3){ Customer = "Jane Doe", EffectiveDate = DateTime.UtcNow.AddDays( 7d ) },
        });*/

        _context.SaveChangesAsync();
        // // Default roles
        // var administratorRole = new IdentityRole("Administrator");
        //
        // if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        // {
        //     await _roleManager.CreateAsync(administratorRole);
        // }
        //
        // // Default users
        // var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
        //
        // if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        // {
        //     await _userManager.CreateAsync(administrator, "Administrator1!");
        //     await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
        // }
        //
        // // Default data
        // // Seed, if necessary
        // if (!_context.TodoLists.Any())
        // {
        //     _context.TodoLists.Add(new TodoList
        //     {
        //         Title = "Todo List",
        //         Items =
        //         {
        //             new TodoItem { Title = "Make a todo list 📃" },
        //             new TodoItem { Title = "Check off the first item ✅" },
        //             new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
        //             new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
        //         }
        //     });
        //
        //     await _context.SaveChangesAsync();
        // }
    }
}
