using LLMAbbyNormal.Domain.Models.NeuralNetwork;
using Xunit;

namespace LLMAbbyNormal.Domain.Test.Models.NeuralNetwork;

public class OutputTest
{
    private readonly Output _sut = new();

    [Fact]
    public void EmitValue_ShouldEmitValueToAllSubscribers()
    {
        // Arrange
        double? firstEmittedValue = null;
        double? secondEmittedValue = null;
        _sut.ValueEmitted += value => firstEmittedValue = value;
        _sut.ValueEmitted += value => secondEmittedValue = value;

        // Act
        _sut.EmitValue(1.5);

        // Assert
        Assert.Equal(1.5, firstEmittedValue);
        Assert.Equal(1.5, secondEmittedValue);
    }

    [Fact]
    public void EmitValue_WhenThereAreNoSubscribers_ShouldNotThrow()
    {
        // Act
        var exception = Record.Exception(() => _sut.EmitValue(1.5));

        // Assert
        Assert.Null(exception);
    }
}
