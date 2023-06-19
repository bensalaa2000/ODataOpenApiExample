namespace Axess.Application.Models;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a product.
/// </summary>
public class ProductDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the product.
	/// </summary>
	/// <value>The product's unique identifier.</value>
	public Guid Code { get; set; }

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
	[ForeignKey(nameof(Supplier))]
	public Guid? SupplierId { get; set; }

	/// <summary>
	/// Gets or sets the associated supplier.
	/// </summary>
	/// <value>The associated supplier.</value>
	public virtual SupplierDto Supplier { get; set; }
}