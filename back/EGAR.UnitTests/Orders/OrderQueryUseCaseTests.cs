using EGAR.Application.Features.Orders.Queries;
using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Enums;
using Moq;
using Xunit;

namespace EGAR.UnitTests.Orders;

public class OrderQueryUseCaseTests
{
    readonly Mock<IOrderRepository> _repoMock;
    readonly OrderQueryHandler _useCase;
    readonly int _existingOrderId = 1;
    readonly Order _existingOrder;

    public OrderQueryUseCaseTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _existingOrder = new Order
        {
            Id = _existingOrderId
        };
        _repoMock
           .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync((int id, CancellationToken _) =>
               id == _existingOrderId ? _existingOrder : null);

        _useCase = new OrderQueryHandler(_repoMock.Object);
    }

    [Fact]
    public async Task WithValidIdAndExistingOrder_ReturnsSuccessWithOrder()
    {
        // Arrange
        var query = new OrderQuery(_existingOrderId);

        // Act
        var result = await _useCase.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(_existingOrderId, result.Value.Id);
    }

    [Theory]
    [InlineData(0, ErrorType.Validation)]   // OrderId isn't correct
    [InlineData(-10, ErrorType.Validation)] // OrderId isn't correct
    [InlineData(100, ErrorType.NotFound)]   // OrderId wasn't found
    public async Task WithInvalidOrMissingOrder_ReturnsExpectedFailure(
        int invalidId,
        ErrorType expectedErrorType)
    {
        // Arrange
        var query = new OrderQuery(invalidId);

        // Act
        var result = await _useCase.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorType, result.ErrorType);
    }
}
