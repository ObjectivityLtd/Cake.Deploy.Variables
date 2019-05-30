namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class ReleaseVariableUInt32Tests : ReleaseVariableGenericBaseTests<uint>
    {
        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("4294967295", 4294967295)]
        public override void AssertSuccess(string variableValue, uint expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-100", "Value was either too large or too small for a UInt32.")]
        [InlineData("8000000000", "Value was either too large or too small for a UInt32.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
