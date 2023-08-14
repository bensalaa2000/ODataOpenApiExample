namespace Axess.Domain.Entities;

/// <summary>
/// Represents an order.
/// </summary>
public class Order : Entity
{
	public Order(Guid id, string customer) : this(id)
	{
		Customer = customer;
	}

	public Order(Guid id) : base(id)
	{
		Customer = string.Empty;
	}

	public Order() : base(Guid.NewGuid())
	{
		Customer = string.Empty;
	}

	/// <summary>
	/// Gets or sets the date and time when the order was created.
	/// </summary>
	/// <value>The order's creation date.</value>
	public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the date and time when the order becomes effective.
	/// </summary>
	/// <value>The order's effective date.</value>
	public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the name of the ordering customer.
	/// </summary>
	/// <value>The name of the customer that placed the order.</value>
	public string Customer { get; set; }

	/// <summary>
	/// Gets or sets a description for the order.
	/// </summary>
	/// <value>The description of the order.</value>
	public string Description { get; set; } = string.Empty;

	private readonly List<LineItem> _lineItems = new();
	public virtual ICollection<LineItem> LineItems => _lineItems.AsReadOnly();
	public void AddLineItem(LineItem entry) => _lineItems.Add(entry);
	public void AddLineItemRange(IEnumerable<LineItem> entries) => _lineItems.AddRange(entries);

}