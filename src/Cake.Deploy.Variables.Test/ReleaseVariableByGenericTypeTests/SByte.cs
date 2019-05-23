using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public class SByte : ReleaseVariableByGenericTypeTests<sbyte>
    {
        [Theory]
        [InlineData("-30", -30)]
        [InlineData("40", 40)]
        public override void AssertSuccess(string variableValue, sbyte expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-1000", "Value was either too large or too small for a signed byte.")]
        [InlineData("2000", "Value was either too large or too small for a signed byte.")]
        [InlineData("ABCD", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
