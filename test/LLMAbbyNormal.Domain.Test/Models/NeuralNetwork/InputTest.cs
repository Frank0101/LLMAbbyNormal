using LLMAbbyNormal.Domain.Models.NeuralNetwork;
using Xunit;

namespace LLMAbbyNormal.Domain.Test.Models.NeuralNetwork;

public class InputTest
{
    private readonly Input _sut = new();

    [Fact]
    public void Constructor_ShouldInitializeDefaultState()
    {
        // Assert
        Assert.False(_sut.HasValue);
        Assert.Equal(1.0, _sut.Weight);
    }

    [Fact]
    public void ReceiveValue_ShouldStoreValueBeforeRaisingValueReceived()
    {
        // Arrange
        var hadValueWhenRaised = false;
        _sut.ValueReceived += () => hadValueWhenRaised = _sut.HasValue;

        // Act
        _sut.ReceiveValue(1.0);

        // Assert
        Assert.True(hadValueWhenRaised);
        Assert.True(_sut.HasValue);
    }

    [Fact]
    public void ReceiveValue_WhenValueIsUnconsumed_ShouldThrowAndPreserveStoredValue()
    {
        // Arrange
        _sut.ReceiveValue(1.0);

        // Act
        var exception = Record.Exception(() => _sut.ReceiveValue(2.0));
        var storedValue = _sut.ConsumeValue();

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal(1.0, storedValue);
    }

    [Fact]
    public void ConsumeValue_ShouldReturnAndClearReceivedValue()
    {
        // Arrange
        _sut.ReceiveValue(1.5);

        // Act
        var value = _sut.ConsumeValue();

        // Assert
        Assert.Equal(1.5, value);
        Assert.False(_sut.HasValue);
    }

    [Fact]
    public void ConsumeValue_WhenNoValueIsAvailable_ShouldThrowInvalidOperationException()
    {
        // Act
        Action act = () => _sut.ConsumeValue();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
