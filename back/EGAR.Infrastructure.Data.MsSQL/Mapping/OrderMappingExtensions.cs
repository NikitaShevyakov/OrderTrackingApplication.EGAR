using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.Infrastructure.Data.MsSQL.Entities;

namespace EGAR.Infrastructure.Data.MsSQL.Mapping;

public static class OrderMappingExtensions
{
    public static Order ToDomain(this OrderEntity entity)
    {
        if (entity == null)
            return null!;

        return new Order
        {
            Id = entity.Id,
            OrderNumber = entity.OrderNumber,
            Description = entity.Description,
            Status = (OrderStatus)entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static OrderEntity ToEntity(this Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        return new OrderEntity
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Description = order.Description,
            Status = (int)order.Status,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }

    public static OrderEntity ToNewEntity(this Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        if (order.Id != 0) throw new InvalidOperationException("Order already has Id");

        return new OrderEntity
        {
            OrderNumber = order.OrderNumber,
            Description = order.Description,
            Status = (int)order.Status,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }
}

