namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

/// <summary>
/// Emits values produced by a neural network element.
/// </summary>
public class Output
{
    /// <summary>
    /// Occurs when a value is emitted.
    /// </summary>
    public event Action<double>? ValueEmitted;

    /// <summary>
    /// Emits a value to all subscribers.
    /// </summary>
    /// <param name="value">The value to emit.</param>
    public void EmitValue(double value)
    {
        ValueEmitted?.Invoke(value);
    }
}
