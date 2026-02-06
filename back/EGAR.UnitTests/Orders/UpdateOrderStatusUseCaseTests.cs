using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.MessageBus.Contracts.Orders;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using EGAR.SharedKernel.Publishers;
using Moq;
using Xunit;

namespace EGAR.UnitTests.Orders;

public class UpdateOrderStatusUseCaseTests
{
    readonly Mock<IOrderRepository> _repoMock;
    readonly Mock<IPublisher> _publisherMock;
    readonly UpdateOrderStatusCommandHandler _useCase;
    readonly int _existingOrderId = 1;
    readonly Order _existingOrder;
    bool _updateShouldSucceed = true;
    bool _publishShouldSucceed = true;

    public UpdateOrderStatusUseCaseTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _publisherMock = new Mock<IPublisher>();

        _existingOrder = new Order
        {
            Id = _existingOrderId,
            Status = OrderStatus.Created,
            CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
            UpdatedAt = DateTimeOffset.UtcNow.AddMinutes(-1)
        };

        _repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken _) =>
                id == _existingOrderId ? _existingOrder : null);

        _repoMock
            .Setup(r => r.UpdateAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order o, CancellationToken _) =>
                _updateShouldSucceed
                    ? Result<Order>.Success(o)
                    : Result<Order>.Failure(ErrorType.Internal, "Something went wrong")
            );

        _publisherMock
            .Setup(p => p.PublishAsync(It.IsAny<OrderStatusChangedEvent>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_publishShouldSucceed);

        _useCase = new UpdateOrderStatusCommandHandler(_repoMock.Object, _publisherMock.Object);

    }

    [Fact]
    public async Task WithValidId_PublishesEventAndReturnsSuccessResult()
    {
        // Arrange
        var newStatus = OrderStatus.Sent;
        var oldUpdatedAt = _existingOrder.UpdatedAt;
        var command = new UpdateOrderStatusCommand(_existingOrderId, newStatus);

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newStatus, result.Value.Status);
        Assert.True(oldUpdatedAt < result.Value.UpdatedAt);
    }

    [Theory]
    [InlineData(-5, true, ErrorType.NotFound)]   // Id isn't correct
    [InlineData(100, true, ErrorType.NotFound)]  // Order not found
    [InlineData(1, false, ErrorType.Internal)]   // UpdateAsync was Failed
    public async Task WhenUpdateOrderStatusFails_ReturnsExpectedFailure(
        int orderId,
        bool updateShouldSucceed,
        ErrorType expectedErrorType)
    {
        // Arrange
        var newStatus = OrderStatus.Sent;
        var updatedAt = _existingOrder.UpdatedAt;
        _updateShouldSucceed = updateShouldSucceed;
        var command = new UpdateOrderStatusCommand(orderId, newStatus);

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorType, result.ErrorType);
        Assert.NotEqual(newStatus, result.Value?.Status);
        Assert.False(updatedAt == result.Value?.UpdatedAt);
    }
}
