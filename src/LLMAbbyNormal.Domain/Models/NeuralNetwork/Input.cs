namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

/// <summary>
/// Receives and stores values for consumption by a neural network element.
/// </summary>
public class Input
{
    private double? _value;

    /// <summary>
    /// Gets a value indicating whether an unconsumed value is available.
    /// </summary>
    public bool HasValue => _value.HasValue;

    /// <summary>
    /// Occurs after a value has been received and stored.
    /// </summary>
    public event Action? ValueReceived;

    /// <summary>
    /// Gets or sets the weight associated with this input.
    /// </summary>
    public double Weight { get; set; } = 1.0;

    /// <summary>
    /// Receives and stores a value, then notifies subscribers.
    /// </summary>
    /// <param name="value">The value to receive.</param>
    /// <exception cref="InvalidOperationException">
    /// An unconsumed value is already stored.
    /// </exception>
    public void ReceiveValue(double value)
    {
        if (HasValue)
            throw new InvalidOperationException(
                "An unconsumed value is already present.");

        _value = value;
        ValueReceived?.Invoke();
    }

    /// <summary>
    /// Returns and clears the stored value.
    /// </summary>
    /// <returns>The stored value.</returns>
    /// <exception cref="InvalidOperationException">
    /// No value is available to consume.
    /// </exception>
    public double ConsumeValue()
    {
        var value = _value ??
                    throw new InvalidOperationException(
                        "No value is available to consume.");
        _value = null;
        return value;
    }
}
