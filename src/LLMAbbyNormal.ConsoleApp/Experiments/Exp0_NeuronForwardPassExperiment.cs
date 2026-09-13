using LLMAbbyNormal.Domain.Models.NeuralNetwork;

namespace LLMAbbyNormal.ConsoleApp.Experiments;

internal static class Exp0_NeuronForwardPassExperiment
{
    // Demonstrates a forward pass through one neuron. With the identity activation,
    // default weights of 1, and default bias of 0, its emitted result is the sum of
    // the three input values.
    internal static void Run()
    {
        var neuron = new Neuron(3, value => value);
        neuron.Output.ValueEmitted += value
            => Console.WriteLine($"Neuron has emitted value: {value}");

        for (var inputIndex = 0; inputIndex < 3; inputIndex++)
        {
            var value = inputIndex + 1;
            Console.WriteLine($"Neuron input #{inputIndex} receiving value: {value}");
            neuron.Inputs[inputIndex].ReceiveValue(value);
        }
    }
}
