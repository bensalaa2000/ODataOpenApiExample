namespace Axess.Application.Models;

/// <summary>
/// Represents a supplier.
/// </summary>
public class SupplierDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the supplier.
	/// </summary>
	/// <value>The supplier's unique identifier.</value>
	public Guid Code { get; set; }

	/// <summary>
	/// Gets or sets the supplier name.
	/// </summary>
	/// <value>The supplier's name.</value>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets products associated with the supplier.
	/// </summary>
	/// <value>The collection of associated products.</value>
	public ICollection<ProductDto>? Products { get; set; }
}