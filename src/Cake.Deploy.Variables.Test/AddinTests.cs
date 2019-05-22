using System;
using System.Collections.Generic;
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

        [Fact]
        public void
            When_VariableDefinedInBaseEnvReferenceOtherVariableOverridenInCurrentEnvironment_Should_ReturnValueFromCurrentEnvironment
            ()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            var referencedVariableName = "envName";
            var baseReferencedValue = "default";

            var currentReferencedValue = "development";

            var variableName = "webSiteName";

            context.ReleaseEnvironment("default")
                .AddVariable(referencedVariableName, baseReferencedValue)
                .AddVariable(variableName, x => x[referencedVariableName]);

            context.ReleaseEnvironment(currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(referencedVariableName, currentReferencedValue);

            //assert
            Assert.Equal(currentReferencedValue, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariableDefinedInBaseEnvReferenceTwoVariables_Should_Return_ConcatOfBaseValueAndCurrentValue()
        {
            //arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);

            //act
            var context = fixture.GetContext();

            var referencedVariableName1 = "var1";
            var baseReferencedValue1 = "default1";
            var referencedVariableName2 = "var2";
            var baseReferencedValue2 = "default2";

            var currentReferencedValue = "development";

            var variableName = "webSiteName";

            context.ReleaseEnvironment("default")
                .AddVariable(referencedVariableName1, baseReferencedValue1)
                .AddVariable(referencedVariableName2, baseReferencedValue2)
                .AddVariable(variableName, x => x[referencedVariableName1] + " " + x[referencedVariableName2]);

            context.ReleaseEnvironment(currentEnvironment)
                .IsBasedOn("default")
                .AddVariable(referencedVariableName1, currentReferencedValue);

            //assert
            Assert.Equal(currentReferencedValue + " " + baseReferencedValue2, context.ReleaseVariable()[variableName]);
        }

        [Fact]
        public void When_VariablesValuesAreConvertible_Should_ReturnTheirsValuesByGenericTypes()
        {
            // arrange
            var currentEnvironment = "dev";
            var fixture = new CakeContextFixture(currentEnvironment);
            var context = fixture.GetContext();

            const string boolValueName = "BoolValueName";
            const string decimalWithPointValueName = "DecimalWithPointValueName";
            const string decimalWithCommaValueName = "DecimalWithCommaValueName";
            const string dateValueName = "DateValueName";
            const string intValueName = "IntValueName";
            const string stringValueName = "StringValueName";

            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(boolValueName, "true")
                .AddVariable(decimalWithPointValueName, "2.54")
                .AddVariable(decimalWithCommaValueName, "2,54")
                .AddVariable(dateValueName, "2019.05.21")
                .AddVariable(intValueName, "111")
                .AddVariable(stringValueName, "someText");

            //act
            bool boolValue = context.ReleaseVariable<bool>(boolValueName);
            decimal decimalWithPointValue = context.ReleaseVariable<decimal>(decimalWithPointValueName);
            decimal decimalWithCommaValue = context.ReleaseVariable<decimal>(decimalWithCommaValueName);
            DateTime dateValue = context.ReleaseVariable<DateTime>(dateValueName);
            int intValue = context.ReleaseVariable<int>(intValueName);
            string stringValue = context.ReleaseVariable<string>(stringValueName);

            //assert
            Assert.Equal(true, boolValue);
            Assert.Equal(2.54m, decimalWithPointValue);
            Assert.Equal(2.54m, decimalWithCommaValue);
            Assert.Equal(new DateTime(2019, 5, 21), dateValue);
            Assert.Equal(111, intValue);
            Assert.Equal("someText", stringValue);
        }

        public void Dispose()
        {
            VariableManager.Clear();
        }
    }
}
