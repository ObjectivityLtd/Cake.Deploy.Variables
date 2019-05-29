namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class Char : ReleaseVariableByGenericTypeTests<char>
    {
        [Theory]
        [InlineData("f", 'f')]
        [InlineData("9", '9')]
        public override void AssertSuccess(string variableValue, char expectedValue)
            => this.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("", "String must be exactly one character long.")]
        [InlineData("someLongValue", "String must be exactly one character long.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => this.AssertFailure(variableValue, expectedErrorMessage);
    }
}
