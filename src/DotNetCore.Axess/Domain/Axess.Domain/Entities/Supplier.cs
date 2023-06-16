namespace Axess.Domain.Entities;

/// <summary>
/// Represents a supplier.
/// </summary>
public class Supplier : Entity
{
    public Supplier(Guid id) : base(id) { }
    /// <summary>
    /// Gets or sets the supplier name.
    /// </summary>
    /// <value>The supplier's name.</value>
    public string Name { get; set; }

    private readonly List<Product> _products = new();
    public virtual ICollection<Product> LineItems => _products.AsReadOnly();
    public void AddProduct(Product product) => _products.Add(product);
    public void AddProductRange(IEnumerable<Product> products) => _products.AddRange(products);
    //public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}