# LLM Abby Normal

LLM Abby Normal is an educational, object-oriented neural-network implementation
written in C#. It favors clear, observable behavior over performance and is
being developed incrementally as a way to explore how neural networks work.

Development progresses through small, ordered experiments. Each experiment builds
on the previous one and makes one more part of a neural network visible before the
model grows more complex.

## Experiments

The experiments live in `LLMAbbyNormal.ConsoleApp/Experiments` and run in order
when the console application starts.

| #   | Experiment                      | What it demonstrates                                                                                                       |
| --- | ------------------------------- | -------------------------------------------------------------------------------------------------------------------------- |
| 0   | `NeuronForwardPassExperiment`   | Sends values through a neuron and observes its output using identity activation, default weights, and default bias.        |
| 1   | `YEqualsTwoXTrainingExperiment` | Trains one neuron to learn `y = 2x` using supervised learning and full-batch gradient descent, then uses it for inference. |

## Project structure

- `LLMAbbyNormal.Domain` contains the neural-network model.
- `LLMAbbyNormal.ConsoleApp` provides an executable for experimenting with the
  model.
- `LLMAbbyNormal.Domain.Test` contains tests for the domain model.

## Build and run

Build the whole solution:

```bash
dotnet build LLMAbbyNormal.sln
```

Run the console example:

```bash
dotnet run --project src/LLMAbbyNormal.ConsoleApp
```

Run all tests:

```bash
dotnet test
```
