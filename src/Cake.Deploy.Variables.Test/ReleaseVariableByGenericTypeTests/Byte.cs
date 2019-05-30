namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class Byte : ReleaseVariableByGenericTypeTests<byte>
    {
        [Theory]
        [InlineData("40", 40)]
        [InlineData("0", 0)]
        public override void AssertSuccess(string variableValue, byte expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-30", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("-1000", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("2000", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("ABCD", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
