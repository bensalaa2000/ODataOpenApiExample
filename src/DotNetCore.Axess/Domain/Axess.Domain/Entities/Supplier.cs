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

    /// <summary>
    /// Gets or sets products associated with the supplier.
    /// </summary>
    /// <value>The collection of associated products.</value>
    //public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    private readonly List<Product> _products = new();
    public virtual ICollection<Product> Products => _products.AsReadOnly();
    public void AddProduct(Product entry) => _products.Add(entry);
    public void AddProductRange(IEnumerable<Product> entries) => _products.AddRange(entries);

}