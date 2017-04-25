using System;
using System.Collections.Generic;
using Cake.Core;
using Xunit;

namespace Cake.Deploy.Variables.Test
{
    public class AddinTests : IDisposable
    {
        [Fact]
        public void When_VariableDefinedInCurrentEnvironment_Should_ReturnItsValue()
        {
            // arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            var variableName = "simpleVariable";
            var variableValue = "simpleVariableValue";

            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(variableName, variableValue);
            
            //assert
            Assert.Equal(variableValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableReferencesOtherVariableInCurrentEnvironment_Should_ReturnOtherVariableValue()
        {
            // arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);
            //act
            var context = fixture.GetContext();

            var baseVariable = "referencedVariable";
            var baseVariableValue = "defaultValue";

            var childVariable = "functionVariable";

            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(baseVariable, baseVariableValue)
                .AddVariable(childVariable, x => x[baseVariable]);

            //assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[childVariable]);
        }

        [Fact]
        public void When_VariableDefinedOnlyInBaseEnvironment_Should_ReturnVariableValueFromTheBaseEnvironment()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            var baseVariable = "baseVariable";
            var baseVariableValue = "defaultValue";

            context.ReleaseEnvironment("default")
                .AddVariable(baseVariable, baseVariableValue);

            context.ReleaseEnvironment(currentEnvironment)
                .IsBasedOn("default");

            //assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[baseVariable]);
        }

        [Fact]
        public void When_VariableDefinedInCurrentEnvironmentButNotInBaseEnv_Should_ReturnVariableValue()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            context.ReleaseEnvironment("default");

            var variableName = "simpleVariable";
            var variableValue = "simpleValue";

            context.ReleaseEnvironment(currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(variableName, variableValue);

            //assert
            Assert.Equal(variableValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableDefinedInCurrentEnvironmentReferencesVariableInBaseEnv_Should_ReturnBaseVariableValue()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            var baseVariable = "referencedVariable";
            var baseVariableValue = "defaultValue";

            context.ReleaseEnvironment("default")
                .AddVariable(baseVariable, baseVariableValue);

            var currentEnvVariable = "childVariable";

            context.ReleaseEnvironment(currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(currentEnvVariable, x => x[baseVariable]);

            //assert
            Assert.Equal(baseVariableValue, context.ReleaseVariable()[currentEnvVariable]);
        }

        [Fact]
        public void When_VariableNotDefined_Should_ThrowAnException()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            context.ReleaseEnvironment("default");

            context.ReleaseEnvironment(currentEnvironment);

            var variableName = "testVariable";

            var exception = Record.Exception(() => context.ReleaseVariable()[variableName]);

            Assert.IsType(typeof(KeyNotFoundException), exception);
        }

        public void Dispose()
        {
            VariableManager.Clear();
        }
    }
}
