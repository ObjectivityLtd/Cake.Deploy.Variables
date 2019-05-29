namespace Cake.Deploy.Variables.Test
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class AddinTests : IDisposable
    {
        private readonly string currentEnvironment = Guid.NewGuid().ToString("N");
        private readonly CakeContextFixture fixture;

        public AddinTests()
        {
            this.fixture = new CakeContextFixture(this.currentEnvironment);
        }

        [Fact]
        public void When_VariableDefinedInCurrentEnvironment_Should_ReturnItsValue()
        {
            // arrange
            var context = this.fixture.GetContext();

            var variableName = "simpleVariable";
            var variableValue = "simpleVariableValue";

            // act
            context.ReleaseEnvironment(this.currentEnvironment)
                .AddVariable(variableName, variableValue);

            // assert
            Assert.Equal(variableValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableReferencesOtherVariableInCurrentEnvironment_Should_ReturnOtherVariableValue()
        {
            // arrange
            var context = this.fixture.GetContext();

            var baseVariable = "referencedVariable";
            var baseVariableValue = "defaultValue";

            var childVariable = "functionVariable";

            // act
            context.ReleaseEnvironment(this.currentEnvironment)
                .AddVariable(baseVariable, baseVariableValue)
                .AddVariable(childVariable, x => x[baseVariable]);

            // assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[childVariable]);
        }

        [Fact]
        public void When_VariableDefinedOnlyInBaseEnvironment_Should_ReturnVariableValueFromTheBaseEnvironment()
        {
            // arrange
            var context = this.fixture.GetContext();

            var baseVariable = "baseVariable";
            var baseVariableValue = "defaultValue";

            // act
            context.ReleaseEnvironment("default")
                .AddVariable(baseVariable, baseVariableValue);

            context.ReleaseEnvironment(this.currentEnvironment)
                .IsBasedOn("default");

            // assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[baseVariable]);
        }

        [Fact]
        public void When_VariableDefinedInCurrentEnvironmentButNotInBaseEnv_Should_ReturnVariableValue()
        {
            // arrange
            var context = this.fixture.GetContext();

            var variableName = "simpleVariable";
            var variableValue = "simpleValue";

            // act
            context.ReleaseEnvironment("default");

            context.ReleaseEnvironment(this.currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(variableName, variableValue);

            // assert
            Assert.Equal(variableValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableDefinedInCurrentEnvironmentReferencesVariableInBaseEnv_Should_ReturnBaseVariableValue()
        {
            // arrange
            var context = this.fixture.GetContext();

            var baseVariable = "referencedVariable";
            var baseVariableValue = "defaultValue";

            var currentEnvVariable = "childVariable";

            // act
            context.ReleaseEnvironment("default")
                .AddVariable(baseVariable, baseVariableValue);

            context.ReleaseEnvironment(this.currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(currentEnvVariable, x => x[baseVariable]);

            // assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[currentEnvVariable]);
        }

        [Fact]
        public void When_VariableNotDefined_Should_ThrowAnException()
        {
            // arrange
            var context = this.fixture.GetContext();

            var variableName = "testVariable";

            // act
            context.ReleaseEnvironment("default");

            context.ReleaseEnvironment(this.currentEnvironment);

            var exception = Record.Exception(() => context.ReleaseVariable()[variableName]);

            Assert.IsType<KeyNotFoundException>(exception);
        }

        [Fact]
        public void When_VariableDefinedInBaseEnvReferenceOtherVariableOverridenInCurrentEnvironment_Should_ReturnValueFromCurrentEnvironment()
        {
            // arrange
            var context = this.fixture.GetContext();

            var referencedVariableName = "envName";
            var baseReferencedValue = "default";

            var currentReferencedValue = "development";

            var variableName = "webSiteName";

            // act
            context.ReleaseEnvironment("default")
                .AddVariable(referencedVariableName, baseReferencedValue)
                .AddVariable(variableName, x => x[referencedVariableName]);

            context.ReleaseEnvironment(this.currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(referencedVariableName, currentReferencedValue);

            // assert
            Assert.Equal(currentReferencedValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableDefinedInBaseEnvReferenceTwoVariables_Should_Return_ConcatOfBaseValueAndCurrentValue()
        {
            // arrange
            var context = this.fixture.GetContext();

            var referencedVariableName1 = "var1";
            var baseReferencedValue1 = "default1";
            var referencedVariableName2 = "var2";
            var baseReferencedValue2 = "default2";

            var currentReferencedValue = "development";

            var variableName = "concatVariable";

            // act
            context.ReleaseEnvironment("default")
                .AddVariable(referencedVariableName1, baseReferencedValue1)
                .AddVariable(referencedVariableName2, baseReferencedValue2)
                .AddVariable(variableName, x => x[referencedVariableName1] + " " + x[referencedVariableName2]);

            context.ReleaseEnvironment(this.currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(referencedVariableName1, currentReferencedValue);

            // assert
            Assert.Equal(currentReferencedValue + " " + baseReferencedValue2, context.ReleaseVariable()[variableName]);
        }

        public void Dispose()
        {
            this.fixture.Dispose();
        }
    }
}
