using LLMAbbyNormal.Domain.Models.NeuralNetwork;

Console.WriteLine("LLM Abby Normal");

var neuron = new Neuron(3);
neuron.Output.ValueEmitted += value
    => Console.WriteLine($"Neuron has emitted value: {value}");


for (var n = 0; n < 3; n++)
{
    var value = n + 1;
    Console.WriteLine($"Neuron input #{n} receiving value: {value}");
    neuron.Inputs[n].ReceiveValue(value);
}
