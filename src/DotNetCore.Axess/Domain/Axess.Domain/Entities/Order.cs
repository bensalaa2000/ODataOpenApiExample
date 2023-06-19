namespace Axess.Domain.Entities;

/// <summary>
/// Represents an order.
/// </summary>
public class Order : Entity
{
	public Order(Guid id) : base(id) { }
	/// <summary>
	/// Gets or sets the date and time when the order was created.
	/// </summary>
	/// <value>The order's creation date.</value>
	public DateTime CreatedDate { get; set; } = DateTime.Now;

	/// <summary>
	/// Gets or sets the date and time when the order becomes effective.
	/// </summary>
	/// <value>The order's effective date.</value>
	public DateTime EffectiveDate { get; set; } = DateTime.Now;

	/// <summary>
	/// Gets or sets the name of the ordering customer.
	/// </summary>
	/// <value>The name of the customer that placed the order.</value>
	public string Customer { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets a description for the order.
	/// </summary>
	/// <value>The description of the order.</value>
	public string Description { get; set; } = string.Empty;

	private readonly List<LineItem> _LineItems = new();
	public virtual ICollection<LineItem> LineItems => _LineItems.AsReadOnly();
	public void AddLineItem(LineItem lineItem) => _LineItems.Add(lineItem);
	public void AddLineItemRange(IEnumerable<LineItem> lineItems) => _LineItems.AddRange(lineItems);
	//public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}