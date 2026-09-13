namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

/// <summary>
/// Consumes weighted input values, applies a bias and activation function,
/// and emits the result.
/// </summary>
public class Neuron
{
    private readonly Func<double, double> _activationFunction;

    /// <summary>
    /// Gets the inputs consumed by the neuron.
    /// </summary>
    public Input[] Inputs { get; }

    /// <summary>
    /// Gets the output through which the neuron emits its result.
    /// </summary>
    public Output Output { get; } = new();

    /// <summary>
    /// Gets or sets the bias added to the weighted sum of the inputs.
    /// </summary>
    public double Bias { get; set; } = 0.0;

    /// <summary>
    /// Creates a neuron with a fixed number of inputs and an activation function.
    /// </summary>
    /// <param name="inputsCount">The number of inputs to create.</param>
    /// <param name="activationFunction">
    /// The function applied to the weighted sum and bias.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="inputsCount"/> is not positive.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="activationFunction"/> is null.
    /// </exception>
    public Neuron(int inputsCount, Func<double, double> activationFunction)
    {
        if (inputsCount <= 0)
            throw new InvalidOperationException(
                "A neuron must have at least one input.");

        ArgumentNullException.ThrowIfNull(activationFunction);

        _activationFunction = activationFunction;

        // Creates the inputs
        Inputs = new Input[inputsCount];
        for (var n = 0; n < Inputs.Length; n++)
        {
            Inputs[n] = new Input();

            // Wires the event
            Inputs[n].ValueReceived += input_received;
        }
    }

    private void input_received()
    {
        // Check that all inputs have a value
        if (!Inputs.All(i => i.HasValue)) return;

        // Calculate the weighted sum of inputs plus bias
        var weightedSum = Inputs.Sum(i => i.ConsumeValue() * i.Weight) + Bias;

        // Apply the activation function and emit the result
        Output.EmitValue(_activationFunction(weightedSum));
    }
}
