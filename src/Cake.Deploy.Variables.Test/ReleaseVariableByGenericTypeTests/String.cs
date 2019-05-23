using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public class String : ReleaseVariableByGenericTypeTests<string>
    {
        [Theory]
        [InlineData("ABCDEFG", "ABCDEFG")]
        [InlineData("1234567", "1234567")]
        public override void AssertSuccess(string variableValue, string expectedValue) 
            => base.AssertSuccess(variableValue, expectedValue);
    }
}
