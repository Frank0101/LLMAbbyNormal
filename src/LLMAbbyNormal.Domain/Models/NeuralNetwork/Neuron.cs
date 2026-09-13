namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

public class Neuron
{
    private readonly Func<double, double> _activationFunction;

    public Input[] Inputs { get; }
    public Output Output { get; } = new();

    public double Bias { get; set; } = 0.0;


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
