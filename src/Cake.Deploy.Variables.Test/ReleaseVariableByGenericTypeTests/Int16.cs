using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public class Int16 : ReleaseVariableByGenericTypeTests<short>
    {
        [Theory]
        [InlineData("-100", -100)]
        [InlineData("4000", 4000)]
        public override void AssertSuccess(string variableValue, short expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("70000", "Value was either too large or too small for an Int16.")]
        [InlineData("-70000", "Value was either too large or too small for an Int16.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
