namespace LLMAbbyNormal.Domain.Models.NeuralNetwork;

public class Neuron
{
    public Input[] Inputs { get; }
    public Output Output { get; } = new();

    public Neuron(int inputsCount)
    {
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

        // Emit the output (sum of inputs for now)
        Output.EmitValue(Inputs.Sum(i => i.ConsumeValue()));
    }
}
