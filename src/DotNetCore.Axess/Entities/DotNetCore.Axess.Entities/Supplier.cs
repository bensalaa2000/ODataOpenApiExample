using DotNetCore.Axess.Domain;

namespace DotNetCore.Axess.Entities;

/// <summary>
/// Represents a supplier.
/// </summary>
public class Supplier : Entity<int>
{
    public Supplier(int id) : base(id) { }
    /// <summary>
    /// Gets or sets the supplier name.
    /// </summary>
    /// <value>The supplier's name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets products associated with the supplier.
    /// </summary>
    /// <value>The collection of associated products.</value>
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}