using Axess.Domain;

namespace DotNetCore.Axess.Entities;

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
    public string Customer { get; set; }

    /// <summary>
    /// Gets or sets a description for the order.
    /// </summary>
    /// <value>The description of the order.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    private readonly List<LineItem> _lineItems = new();
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<LineItem> LineItems => _lineItems.AsReadOnly();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    public void AddLineItem(LineItem entry)
    {
        _lineItems.Add(entry);
        // Events.Add(new EntryAddedEvent(this.Id, entry));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entries"></param>
    public void AddRange(IEnumerable<LineItem> entries)
    {
        _lineItems.AddRange(entries);
        // Events.Add(new EntryAddedEvent(this.Id, entry));
    }

    /// <summary>
    /// Gets a list of line items in the order.
    /// </summary>
    /// <value>The <see cref="IList{T}">list</see> of order <see cref="LineItem">line items</see>.</value>
    //public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}