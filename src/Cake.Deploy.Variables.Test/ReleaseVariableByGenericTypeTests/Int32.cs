namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class Int32 : ReleaseVariableByGenericTypeTests<int>
    {
        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-85000", -85000)]
        public override void AssertSuccess(string variableValue, int expectedValue)
            => this.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("-4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => this.AssertFailure(variableValue, expectedErrorMessage);
    }
}
