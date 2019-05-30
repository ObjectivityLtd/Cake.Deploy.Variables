namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class ReleaseVariableUInt64Tests : ReleaseVariableGenericBaseTests<ulong>
    {
        [Theory]
        [InlineData("18446744073709551615", 18446744073709551615)]
        [InlineData("4294967295", 4294967295)]
        public override void AssertSuccess(string variableValue, ulong expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-100", "Value was either too large or too small for a UInt64.")]
        [InlineData("20000000000000000000", "Value was either too large or too small for a UInt64.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
