using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public class Char : ReleaseVariableByGenericTypeTests<char>
    {
        [Theory]
        [InlineData("f", 'f')]
        [InlineData("9", '9')]
        public override void AssertSuccess(string variableValue, char expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("", "String must be exactly one character long.")]
        [InlineData("someLongValue", "String must be exactly one character long.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
