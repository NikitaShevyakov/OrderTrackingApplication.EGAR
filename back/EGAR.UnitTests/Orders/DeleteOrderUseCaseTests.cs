using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using Moq;
using Xunit;

namespace EGAR.UnitTests.Orders;

public class DeleteOrderUseCaseTests
{
    readonly Mock<IOrderRepository> _repoMock;
    readonly DeleteOrderCommandHandler _useCase;
    readonly int _defaultOrderId = 1;
    readonly Order _defaultOrder;

    public DeleteOrderUseCaseTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _defaultOrder = new Order { Id = _defaultOrderId };

        _repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken _) =>
                id == _defaultOrderId ? _defaultOrder : null);

        _repoMock
            .Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken _) => id == _defaultOrderId
                ? Result.Success()
                : Result.Failure(ErrorType.NotFound, "Order not found"));

        _useCase = new DeleteOrderCommandHandler(_repoMock.Object);
    }

    [Fact]
    public async Task WithValidId_DeletesOrderAndReturnsSuccess()
    {
        // Arrange
        var command = new DeleteOrderCommand(_defaultOrderId);

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(-5)]  //Order Id isn't correct
    [InlineData(100)] //Order Id isn't exist
    public async Task WhenOrderIdNotValid_ReturnsValidationFailure(int missingOrderId)
    {
        // Arrange
        var command = new DeleteOrderCommand(missingOrderId);

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }
}
