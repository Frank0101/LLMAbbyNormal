namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

/// <summary>
/// Emits values produced by a neural network element.
/// </summary>
public class Output
{
    public event Action<double>? ValueEmitted;

    public void EmitValue(double value)
    {
        ValueEmitted?.Invoke(value);
    }
}
