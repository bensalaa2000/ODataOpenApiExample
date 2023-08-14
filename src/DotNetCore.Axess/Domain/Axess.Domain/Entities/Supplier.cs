namespace Axess.Domain.Entities;

/// <summary>
/// Represents a supplier.
/// </summary>
public class Supplier : Entity
{
    public Supplier(Guid id) : base(id)
    {
        this.Name = string.Empty;
    }

    public Supplier(Guid id, string name) : base(id)
    {
        this.Name = name;
    }
    /// <summary>
    /// Gets or sets the supplier name.
    /// </summary>
    /// <value>The supplier's name.</value>
    public string Name { get; set; }

    private readonly List<Product> _products = new();
    public virtual ICollection<Product> Products => _products.AsReadOnly();
    public void AddProduct(Product entry) => _products.Add(entry);
    public void AddProductRange(IEnumerable<Product> entries) => _products.AddRange(entries);

}