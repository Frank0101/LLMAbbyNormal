using LLMAbbyNormal.Domain.Models.NeuralNetwork;

WriteColouredLine("LLM Abby Normal", ConsoleColor.Green);

(string Name, Action Run)[] experiments =
[
    (nameof(RunNeuronForwardPassExperiment), RunNeuronForwardPassExperiment),
    (nameof(RunYEqualsTwoXTrainingExperiment), RunYEqualsTwoXTrainingExperiment)
];

foreach (var experiment in experiments)
{
    Console.WriteLine();
    Console.WriteLine(new string('-', experiment.Name.Length));
    WriteColouredLine(experiment.Name, ConsoleColor.Yellow);
    Console.WriteLine(new string('-', experiment.Name.Length));
    experiment.Run();
}

// Demonstrates a forward pass through one neuron by providing
// all its input values and observing the emitted result.
static void RunNeuronForwardPassExperiment()
{
    var neuron = new Neuron(3, value => value);
    neuron.Output.ValueEmitted += value
        => Console.WriteLine($"Neuron has emitted value: {value}");

    for (var n = 0; n < 3; n++)
    {
        var value = n + 1;
        Console.WriteLine($"Neuron input #{n} receiving value: {value}");
        neuron.Inputs[n].ReceiveValue(value);
    }
}

// Explores training one neuron to infer the relationship y = 2x.
static void RunYEqualsTwoXTrainingExperiment()
{
}

static void WriteColouredLine(string text, ConsoleColor consoleColor)
{
    var originalColor = Console.ForegroundColor;
    Console.ForegroundColor = consoleColor;
    Console.WriteLine(text);
    Console.ForegroundColor = originalColor;
}
