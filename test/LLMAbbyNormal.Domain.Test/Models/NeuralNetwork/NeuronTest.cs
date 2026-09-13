using LLMAbbyNormal.Domain.Models.NeuralNetwork;
using Xunit;

namespace LLMAbbyNormal.Domain.Test.Models.NeuralNetwork;

public class NeuronTest
{
    [Fact]
    public void Constructor_ShouldCreateRequestedNumberOfInputs()
    {
        // Act
        var sut = new Neuron(3, value => value);

        // Assert
        Assert.Equal(3, sut.Inputs.Length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WhenInputCountIsNotPositive_ShouldThrowInvalidOperationException(
        int inputsCount)
    {
        // Act
        var act = () => new Neuron(inputsCount, value => value);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Constructor_WhenActivationFunctionIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        var act = () => new Neuron(1, null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void ReceiveValue_WhenAllInputsHaveValues_ShouldEmitActivatedWeightedSumWithBias()
    {
        // Arrange
        var sut = new Neuron(3, value => value * 2.0)
        {
            Bias = 0.5
        };
        sut.Inputs[0].Weight = 0.5;
        sut.Inputs[1].Weight = -1.0;
        sut.Inputs[2].Weight = 2.0;

        double? emittedValue = null;
        sut.Output.ValueEmitted += value => emittedValue = value;

        // Act
        sut.Inputs[0].ReceiveValue(2.0);
        sut.Inputs[1].ReceiveValue(3.0);
        sut.Inputs[2].ReceiveValue(4.0);

        // Assert
        Assert.Equal(13.0, emittedValue);
    }

    [Fact]
    public void ReceiveValue_WhenAllInputsHaveValues_ShouldConsumeAllInputValues()
    {
        // Arrange
        var sut = new Neuron(2, value => value);

        // Act
        sut.Inputs[0].ReceiveValue(1.0);
        sut.Inputs[1].ReceiveValue(2.0);

        // Assert
        Assert.All(sut.Inputs, input => Assert.False(input.HasValue));
    }

    [Fact]
    public void ReceiveValue_WhenNotAllInputsHaveValues_ShouldNotEmitValue()
    {
        // Arrange
        var sut = new Neuron(2, value => value);
        var valueWasEmitted = false;
        sut.Output.ValueEmitted += _ => valueWasEmitted = true;

        // Act
        sut.Inputs[0].ReceiveValue(1.0);

        // Assert
        Assert.False(valueWasEmitted);
    }
}
