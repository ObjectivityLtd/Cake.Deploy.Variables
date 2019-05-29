namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class String : ReleaseVariableByGenericTypeTests<string>
    {
        [Theory]
        [InlineData("ABCDEFG", "ABCDEFG")]
        [InlineData("1234567", "1234567")]
        public override void AssertSuccess(string variableValue, string expectedValue)
            => this.AssertSuccess(variableValue, expectedValue);
    }
}
