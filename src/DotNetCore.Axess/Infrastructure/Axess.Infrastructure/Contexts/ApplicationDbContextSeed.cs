using Axess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;

public static class ApplicationDbContextSeed
{
    public static void Seed(this ModelBuilder builder) => builder.SeedAddresses()/*.SeedOrders().SeedPersons().SeedSuppliers()*/;

    private static ModelBuilder SeedOrders(this ModelBuilder builder)
    {
        builder.Entity<Order>(entity => entity.HasData(new Order(Guid.NewGuid())
        {
            Customer = "John Doe",
            CreatedDate = DateTime.Now,
            Description = "A man",
            EffectiveDate = DateTime.Now
        }, new Order(Guid.NewGuid())
        {
            Customer = "Jane Doe",
            Description = "A Woman",
        }));
        return builder;
    }

    private static ModelBuilder SeedAddresses(this ModelBuilder builder)
    {
        builder.Entity<Address>(entity => entity.HasData(new Address(Guid.NewGuid())
        {
            Street = "1 Microsoft Way",
            City = "Redmond",
            State = "WA",
            ZipCode = "98052"
        }, new Address(Guid.NewGuid())
        {
            Street = "123 Some Place",
            City = "Seattle",
            State = "WA",
            ZipCode = "98101"
        }));
        return builder;
    }

    private static ModelBuilder SeedPersons(this ModelBuilder builder)
    {
        builder.Entity<Person>(entity => entity.HasData(new Person(Guid.NewGuid())
        {
            FirstName = "John",
            LastName = "Doe",
            WorkAddressId = Guid.NewGuid(),
            WorkAddress = new Address(Guid.NewGuid())
            {
                Street = "1 Microsoft Way",
                City = "Redmond",
                State = "WA",
                ZipCode = "98052"
            }
        }, new Person(Guid.NewGuid())
        {
            FirstName = "Jane",
            LastName = "Doe",
            HomeAddressId = 41,
            HomeAddress = new Address(Guid.NewGuid())
            {

                Street = "123 Some Place",
                City = "Seattle",
                State = "WA",
                ZipCode = "98101"
            }
        }));
        return builder;
    }
    private static ModelBuilder SeedSuppliers(this ModelBuilder builder)
    {
        builder.Entity<Supplier>(entity => entity.HasData(NewSupplier(Guid.NewGuid()), NewSupplier(Guid.NewGuid()), NewSupplier(Guid.NewGuid())));
        return builder;
    }

    private static Supplier NewSupplier(Guid id) =>
        new Supplier(id)
        {
            Name = "Supplier " + id.ToString(),
            Products = new List<Product>()
            {
                new Product(id)
                {
                    Name = "Product "  + id.ToString(),
                    Category = "Test",
                    Price = 10,
                    SupplierId = Guid.NewGuid(),
                },
            },
        };
}
