using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using Moq;
using Xunit;

namespace EGAR.UnitTests.Orders;

public class CreateOrderUseCaseTests
{
    readonly Mock<IOrderRepository> _repoMock;
    readonly CreateOrderCommandHandler _useCase;

    public CreateOrderUseCaseTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order order, CancellationToken _) => Result<Order>.Success(order));
        _useCase = new CreateOrderCommandHandler(_repoMock.Object);
    }

    [Fact]
    public async Task WithValidData_ReturnsSuccessResult()
    {
        // Arrange
        var command = new CreateOrderCommand(
            OrderNumber: "ORD-001",
            Description: "Test order"
        );

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(null, "Some description")]      // OrderNumber = null
    [InlineData("", "Some description")]        // OrderNumber = empty string
    [InlineData("   ", "Some description")]     // OrderNumber = whitespace
    [InlineData("ORD-001", null)]               // Description = null
    [InlineData("ORD-001", "")]                 // Description = empty string
    [InlineData("ORD-001", "   ")]              // Description = whitespace
    public async Task WithEmptyOrNullFields_ReturnsFailureResult(string? orderNumber, string? description)
    {
        // Arrange
        var command = new CreateOrderCommand(
            OrderNumber: orderNumber,
            Description: description
        );

        // Act
        var result = await _useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }
}
