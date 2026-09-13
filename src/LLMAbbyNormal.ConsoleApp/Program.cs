using LLMAbbyNormal.ConsoleApp.Experiments;

WriteColouredLine("LLM Abby Normal", ConsoleColor.Green);

(string Name, Action Run)[] experiments =
[
    (nameof(Exp0_NeuronForwardPassExperiment), Exp0_NeuronForwardPassExperiment.Run),
    (nameof(Exp1_YEqualsTwoXTrainingExperiment), Exp1_YEqualsTwoXTrainingExperiment.Run)
];

foreach (var experiment in experiments)
{
    Console.WriteLine();
    Console.WriteLine(new string('-', experiment.Name.Length));
    WriteColouredLine(experiment.Name, ConsoleColor.Yellow);
    Console.WriteLine(new string('-', experiment.Name.Length));
    experiment.Run();
}

static void WriteColouredLine(string text, ConsoleColor consoleColor)
{
    var originalColor = Console.ForegroundColor;
    Console.ForegroundColor = consoleColor;
    Console.WriteLine(text);
    Console.ForegroundColor = originalColor;
}
