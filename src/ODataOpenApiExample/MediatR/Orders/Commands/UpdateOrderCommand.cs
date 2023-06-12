namespace Axess.MediatR.Order.Commands;

using Axess.Architecture.Models;
using global::MediatR;
using System.Collections.Generic;

public class UpdateOrderCommand : IRequest<Order>
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime EffectiveDate { get; set; } = DateTime.Now;
    public string Customer { get; set; }
    public string Description { get; set; }
    public virtual IList<LineItem> LineItems { get; } = new List<LineItem>();
}
