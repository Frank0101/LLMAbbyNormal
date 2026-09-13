# Coding Guidelines

## General principles

- Prefer clarity and visibility of the model's mechanics over efficiency.
- Develop the model incrementally and avoid introducing abstractions before they are needed.
- Keep classes focused on their stated responsibilities.
- Follow the existing project structure, naming style, and namespace hierarchy.

## Tests

- Use xUnit v3.
- Put tests in the test project associated with the production project.
- Mirror the production folder structure inside the test project. For example:
  - `src/LLMAbbyNormal.Domain/Models/NeuralNetwork/Input.cs`
  - `test/LLMAbbyNormal.Domain.Test/Models/NeuralNetwork/InputTest.cs`
- Name a test class by appending `Test` to the production class name, such as `InputTest`.
- Name test methods using:

  `{MethodName}_When{Condition}_Should{Expectation}`

  Omit `When{Condition}` when no distinct condition is needed:

  `{MethodName}_Should{Expectation}`

- Use the property name for property tests and `Constructor` for constructor tests.
- Order tests to match the production class:
  1. Properties, in declaration order.
  2. Constructor behavior.
  3. Methods, in declaration order.
- Within the tests for the same property, constructor, or method, put cases in this order:
  1. Happy path.
  2. Alternative conditions.
  3. Error conditions.
- Structure each test using clearly marked `Arrange`, `Act`, and `Assert` sections. Omit a section when it genuinely has no work, such as a property-default test with only an assertion.
- Use `_sut` (system under test) for a shared instance of the class being tested.
- Test observable behavior through the class's public API.
- Keep tests independent and deterministic.
- Aim for high behavioral coverage with the minimum number of tests. Before adding a test, check whether:
  - Its assertions can extend an existing test that uses the same Arrange and Act and verifies the same behavior.
  - The behavior can be covered by adding data to an existing `[Theory]` instead of creating another test method.
- Combine closely related expectations from the same action, but keep unrelated behaviors and distinct failure paths in separate tests.
- Use `[Fact]` for a single case and `[Theory]` with data attributes for the same behavior across multiple inputs.
- Run the complete suite with `dotnet test` after changing production or test code.
