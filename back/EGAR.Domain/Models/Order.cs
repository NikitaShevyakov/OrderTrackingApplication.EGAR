using EGAR.Domain.Enums;

namespace EGAR.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = default!;
    public string Description { get; set; } = default!;
    public OrderStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

}
