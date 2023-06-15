using Axess.Domain;

namespace Axess.Domain.Entities;


/// <summary>
/// Represents the line item on an order.
/// </summary>
public class LineItem : Entity
{
    public LineItem(Guid id) : base(id) { }

    /// <summary>
    /// Gets or sets the line item description.
    /// </summary>
    /// <value>The line item description.</value>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the line item quantity.
    /// </summary>
    /// <value>The line item quantity.</value>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the line item unit price.
    /// </summary>
    /// <value>The line item unit price.</value>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the line item is fulfilled.
    /// </summary>
    /// <value>True if the line item is fulfilled; otherwise, false.</value>
    public bool Fulfilled { get; set; }

    public Guid OrderId { get; set; }

    public Order Order { get; set; } //= Order.Create(1);

}