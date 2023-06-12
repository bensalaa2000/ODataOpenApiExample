using DotNetCore.Axess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;

public static class ApplicationDbContextSeed
{
    public static void Seed(this ModelBuilder builder) => builder.SeedAddresses()/*.SeedOrders().SeedPersons().SeedSuppliers()*/;

    private static ModelBuilder SeedOrders(this ModelBuilder builder)
    {
        builder.Entity<Order>(entity => entity.HasData(new Order(1)
        {
            Customer = "John Doe",
            CreatedDate = DateTime.Now,
            Description = "A man",
            EffectiveDate = DateTime.Now
        }, new Order(2)
        {
            Customer = "Jane Doe",
            Description = "A Woman",
        }));
        return builder;
    }

    private static ModelBuilder SeedAddresses(this ModelBuilder builder)
    {
        builder.Entity<Address>(entity => entity.HasData(new Address(42)
        {
            Street = "1 Microsoft Way",
            City = "Redmond",
            State = "WA",
            ZipCode = "98052"
        }, new Address(41)
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
        builder.Entity<Person>(entity => entity.HasData(new Person(1)
        {
            FirstName = "John",
            LastName = "Doe",
            WorkAddressId = 42,
            WorkAddress = new Address(42)
            {
                Street = "1 Microsoft Way",
                City = "Redmond",
                State = "WA",
                ZipCode = "98052"
            }
        }, new Person(2)
        {
            FirstName = "Jane",
            LastName = "Doe",
            HomeAddressId = 41,
            HomeAddress = new Address(41)
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
        builder.Entity<Supplier>(entity => entity.HasData(NewSupplier(1), NewSupplier(2), NewSupplier(3)));
        return builder;
    }

    private static Supplier NewSupplier(int id) =>
        new Supplier(id)
        {
            Name = "Supplier " + id.ToString(),
            Products = new List<Product>()
            {
                new Product(id)
                {
                    Name = "Product "  + id.ToString(),
                    Category = "Test",
                    Price = id,
                    SupplierId = id,
                },
            },
        };
}
