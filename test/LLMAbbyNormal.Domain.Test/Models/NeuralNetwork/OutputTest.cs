using LLMAbbyNormal.Domain.Models.NeuralNetwork;
using Xunit;

namespace LLMAbbyNormal.Domain.Test.Models.NeuralNetwork;

public class OutputTest
{
    private readonly Output _sut = new();

    [Fact]
    public void EmitValue_ShouldEmitValue()
    {
        // Arrange
        double? emittedValue = null;
        _sut.ValueEmitted += value => emittedValue = value;

        // Act
        _sut.EmitValue(1.5);

        // Assert
        Assert.Equal(1.5, emittedValue);
    }
}
