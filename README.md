# LLM Abby Normal

LLM Abby Normal is an educational, object-oriented neural-network implementation
written in C#. It favors clear, observable behavior over performance and is
being developed incrementally as a way to explore how neural networks work.

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
