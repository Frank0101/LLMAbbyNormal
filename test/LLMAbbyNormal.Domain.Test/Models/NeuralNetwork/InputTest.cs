using LLMAbbyNormal.Domain.Models.NeuralNetwork;
using Xunit;

namespace LLMAbbyNormal.Domain.Test.Models.NeuralNetwork;

public class InputTest
{
    private readonly Input _sut = new();

    [Fact]
    public void HasValue_WhenCreated_ShouldBeFalse()
    {
        // Assert
        Assert.False(_sut.HasValue);
    }

    [Fact]
    public void Weight_WhenCreated_ShouldBeOne()
    {
        // Assert
        Assert.Equal(1.0, _sut.Weight);
    }

    [Fact]
    public void ReceiveValue_ShouldRaiseValueReceived()
    {
        // Arrange
        var wasRaised = false;
        _sut.ValueReceived += () => wasRaised = true;

        // Act
        _sut.ReceiveValue(1.0);

        // Assert
        Assert.True(wasRaised);
    }

    [Fact]
    public void ReceiveValue_ShouldStoreValue()
    {
        // Act
        _sut.ReceiveValue(1.0);

        // Assert
        Assert.True(_sut.HasValue);
    }

    [Fact]
    public void ReceiveValue_WhenValueIsUnconsumed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _sut.ReceiveValue(1.0);

        // Act
        var act = () => _sut.ReceiveValue(2.0);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void ConsumeValue_ShouldReturnReceivedValue()
    {
        // Arrange
        _sut.ReceiveValue(1.5);

        // Act
        var value = _sut.ConsumeValue();

        // Assert
        Assert.Equal(1.5, value);
    }

    [Fact]
    public void ConsumeValue_ShouldClearStoredValue()
    {
        // Arrange
        _sut.ReceiveValue(1.0);

        // Act
        _sut.ConsumeValue();

        // Assert
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
