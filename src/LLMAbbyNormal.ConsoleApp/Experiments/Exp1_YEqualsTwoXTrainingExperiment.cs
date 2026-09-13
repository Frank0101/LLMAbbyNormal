using LLMAbbyNormal.Domain.Models.NeuralNetwork;

namespace LLMAbbyNormal.ConsoleApp.Experiments;

internal static class Exp1_YEqualsTwoXTrainingExperiment
{
    // Trains one neuron to infer the relationship y = 2x, then checks its prediction
    // for a value that does not appear in the training dataset.
    internal static void Run()
    {
        const double inferenceX = 4.0;
        const double expectedY = 8.0;

        var neuron = new Neuron(1, value => value);

        // Output emission is synchronous, so this variable contains the result of
        // the most recent forward pass as soon as ReceiveValue returns.
        var predictedY = 0.0;
        neuron.Output.ValueEmitted += value => predictedY = value;

        // With the default weight of 1 and bias of 0, the untrained neuron returns
        // X unchanged and therefore cannot yet infer the expected value.
        neuron.Inputs[0].ReceiveValue(inferenceX);
        Console.WriteLine($"Before training: X = {inferenceX}");
        Console.WriteLine(
            $"Expected Y = {expectedY}, neuron output = {predictedY}");

        // ---------------
        // Training phase
        // ---------------

        Console.WriteLine();

        // Every example follows y = 2x. Including the origin helps the neuron learn
        // that this relationship needs a bias of 0.
        (double X, double ExpectedY)[] trainingDataset =
        [
            (0.0, 0.0),
            (1.0, 2.0),
            (2.0, 4.0),
            (3.0, 6.0)
        ];

        // An epoch is one complete pass over the dataset. Repeating it gives the
        // parameters time to converge. The learning rate controls the size of each
        // update: too small learns slowly, while too large can overshoot.
        const int epochs = 1_000;
        const double learningRate = 0.1;

        for (var epoch = 0; epoch < epochs; epoch++)
        {
            // Begin each epoch with no accumulated loss or gradients. These values
            // will describe the performance of the current parameters across the
            // complete dataset.
            var totalLoss = 0.0;
            var totalWeightGradient = 0.0;
            var totalBiasGradient = 0.0;

            // For every example, perform a forward pass and measure its signed error.
            // Half-squared error measures the loss and has `error` as its derivative.
            // Because the activation is the identity function, the resulting local
            // gradients are `error * X` for the weight and `error` for the bias.
            foreach (var example in trainingDataset)
            {
                neuron.Inputs[0].ReceiveValue(example.X);
                var error = predictedY - example.ExpectedY;

                totalLoss += 0.5 * error * error;
                totalWeightGradient += error * example.X;
                totalBiasGradient += error;
            }

            // Average the accumulated values so every example contributes equally,
            // then move each parameter in the direction opposite its gradient to
            // reduce the loss.
            var averageLoss = totalLoss / trainingDataset.Length;
            var averageWeightGradient = totalWeightGradient / trainingDataset.Length;
            var averageBiasGradient = totalBiasGradient / trainingDataset.Length;

            neuron.Inputs[0].Weight -= learningRate * averageWeightGradient;
            neuron.Bias -= learningRate * averageBiasGradient;

            // Show the loss periodically so its decrease is visible without printing
            // all 1,000 epochs.
            if (epoch % 100 == 0 || epoch == epochs - 1)
                Console.WriteLine($"Epoch {epoch}: average loss = {averageLoss}");
        }

        // ---------------
        // Inference phase
        // ---------------

        Console.WriteLine();

        // Run the original, unseen value through the trained neuron and display both
        // its prediction and the parameters it learned.
        neuron.Inputs[0].ReceiveValue(inferenceX);
        Console.WriteLine($"After training: X = {inferenceX}");
        Console.WriteLine(
            $"Expected Y = {expectedY}, neuron output = {predictedY}");

        Console.WriteLine();
        Console.WriteLine($"Weight: {neuron.Inputs[0].Weight}");
        Console.WriteLine($"Bias: {neuron.Bias}");
    }
}
