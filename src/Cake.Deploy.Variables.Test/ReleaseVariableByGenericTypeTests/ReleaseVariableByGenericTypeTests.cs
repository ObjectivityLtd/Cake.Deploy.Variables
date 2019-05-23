using System;
using FluentAssertions;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public abstract class ReleaseVariableByGenericTypeTests<T> : IDisposable where T : IConvertible
    {
        public virtual void AssertSuccess(string variableValue, T expectedValue)
        {
            // arrange
            const string currentEnvironment = "dev";
            var context = new CakeContextFixture(currentEnvironment).GetContext();

            const string variableName = "SomeSimpleValue";
            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(variableName, variableValue);

            // act
            var value = context.ReleaseVariable<T>(variableName);

            // assert
            value.Should().Be(expectedValue);
        }

        public virtual void AssertFailure(string variableValue, string expectedErrorMessage)
        {
            // arrange
            const string currentEnvironment = "dev";
            var context = new CakeContextFixture(currentEnvironment).GetContext();

            const string variableName = "SomeSimpleValue";
            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(variableName, variableValue);

            // act
            Action invalidAction = () => context.ReleaseVariable<T>(variableName);

            // assert
            invalidAction.Should().Throw<Exception>().WithMessage(expectedErrorMessage);
        }

        public void Dispose()
        {
            VariableManager.Clear();
        }
    }
}
