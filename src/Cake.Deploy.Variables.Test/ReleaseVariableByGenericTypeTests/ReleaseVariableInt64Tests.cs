namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class ReleaseVariableInt64Tests : ReleaseVariableGenericBaseTests<long>
    {
        [Theory]
        [InlineData("-2147483647", -2147483647)]
        [InlineData("9223372036854775807", 9223372036854775807)]
        public override void AssertSuccess(string variableValue, long expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("18446744073709551615", "Value was either too large or too small for an Int64.")]
        [InlineData("-10000000000000000000", "Value was either too large or too small for an Int64.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
