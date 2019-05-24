namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class UInt16 : ReleaseVariableByGenericTypeTests<ushort>
    {
        [Theory]
        [InlineData("5000", 5000)]
        [InlineData("65535", 65535)]
        public override void AssertSuccess(string variableValue, ushort expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-1000", "Value was either too large or too small for a UInt16.")]
        [InlineData("-30535", "Value was either too large or too small for a UInt16.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
