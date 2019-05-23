using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public enum TestEnum
    {
        FirstValue,
        SecondValue,
        ThirdValue
    }

    public class Enum : ReleaseVariableByGenericTypeTests<TestEnum>
    {
        [Theory]
        [InlineData("0", TestEnum.FirstValue)]
        [InlineData("2", TestEnum.ThirdValue)]
        [InlineData("SecondValue", TestEnum.SecondValue)]
        public override void AssertSuccess(string variableValue, TestEnum expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-1", "Requested value '-1' was not found.")]
        [InlineData("100", "Requested value '100' was not found.")]
        [InlineData("Value2", "Requested value 'Value2' was not found.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
