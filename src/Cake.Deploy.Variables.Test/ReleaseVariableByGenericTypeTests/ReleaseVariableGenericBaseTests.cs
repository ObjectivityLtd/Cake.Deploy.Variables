namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using System;
    using System.Globalization;
    using FluentAssertions;

    public abstract class ReleaseVariableGenericBaseTests<T>
        where T : IConvertible
    {
        public virtual void AssertSuccess(string variableValue, T expectedValue)
        {
            // arrange
            string currentEnvironment = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
            using (var fixture = new CakeContextFixture(currentEnvironment))
            {
                string variableName = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
                fixture.Context.ReleaseEnvironment(currentEnvironment)
                    .AddVariable(variableName, variableValue);

                // act
                var value = fixture.Context.ReleaseVariable<T>(variableName);

                // assert
                value.Should().Be(expectedValue);
            }
        }

        public virtual void AssertFailure(string variableValue, string expectedErrorMessage)
        {
            // arrange
            string currentEnvironment = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
            using (var fixture = new CakeContextFixture(currentEnvironment))
            {
                string variableName = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
                fixture.Context.ReleaseEnvironment(currentEnvironment)
                    .AddVariable(variableName, variableValue);

                // act
                Action invalidAction = () => fixture.Context.ReleaseVariable<T>(variableName);

                // assert
                invalidAction.Should().Throw<Exception>().WithMessage(expectedErrorMessage);
            }
        }
    }
}
