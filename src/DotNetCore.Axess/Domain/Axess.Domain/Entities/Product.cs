namespace Axess.Domain.Entities;

/// <summary>
/// Represents a product.
/// </summary>
public class Product : Entity
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	public Product(Guid id) : base(id) { }
	/// <summary>
	/// Gets or sets the product name.
	/// </summary>
	/// <value>The product's name.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the product price.
	/// </summary>
	/// <value>The price's name.</value>
	public decimal Price { get; set; }

	/// <summary>
	/// Gets or sets the product category.
	/// </summary>
	/// <value>The category's name.</value>
	public string Category { get; set; }

	/// <summary>
	/// Gets or sets the associated supplier identifier.
	/// </summary>
	/// <value>The associated supplier identifier.</value>
	public Guid? SupplierId { get; set; }

	/// <summary>
	/// Gets or sets the associated supplier.
	/// </summary>
	/// <value>The associated supplier.</value>
	public virtual Supplier Supplier { get; set; }
}