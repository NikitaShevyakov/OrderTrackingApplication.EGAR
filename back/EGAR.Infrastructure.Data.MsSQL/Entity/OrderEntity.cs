namespace EGAR.Infrastructure.Data.MsSQL.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
