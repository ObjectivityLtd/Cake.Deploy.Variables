namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class Boolean : ReleaseVariableByGenericTypeTests<bool>
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public override void AssertSuccess(string variableValue, bool expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("23423", "String was not recognized as a valid Boolean.")]
        [InlineData("T", "String was not recognized as a valid Boolean.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}