namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

/// <summary>
/// Receives and stores values for consumption by a neural network element.
/// </summary>
public class Input
{
    private double? _value;

    public bool HasValue => _value.HasValue;
    public event Action? ValueReceived;

    public double Weight { get; set; } = 1.0;

    public void ReceiveValue(double value)
    {
        if (HasValue)
            throw new InvalidOperationException(
                "An unconsumed value is already present.");

        _value = value;
        ValueReceived?.Invoke();
    }

    public double ConsumeValue()
    {
        var value = _value ??
                    throw new InvalidOperationException(
                        "No value is available to consume.");
        _value = null;
        return value;
    }
}
