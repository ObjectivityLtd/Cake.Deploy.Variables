namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class ReleaseVariableDecimalTests : ReleaseVariableGenericBaseTests<decimal>
    {
        [Theory]
        [InlineData("3.14", 3.14)]
        [InlineData("-90.1234", -90.1234)]
        [InlineData("1.23456784789", 1.23456784789)]
        [InlineData("123456.789798", 123456.789798)]
        [InlineData("123,567", 123.567)]
        [InlineData("123.567", 123.567)]
        public override void AssertSuccess(string variableValue, decimal expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-14.14,56", "Input string was not in a correct format.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
