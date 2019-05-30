namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public enum EnumUnderTest
    {
        FirstValue,
        SecondValue,
        ThirdValue,
    }

    public class ReleaseVariableEnumTests : ReleaseVariableGenericBaseTests<EnumUnderTest>
    {
        [Theory]
        [InlineData("0", EnumUnderTest.FirstValue)]
        [InlineData("2", EnumUnderTest.ThirdValue)]
        [InlineData("SecondValue", EnumUnderTest.SecondValue)]
        public override void AssertSuccess(string variableValue, EnumUnderTest expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("-1", "Requested value '-1' was not found.")]
        [InlineData("100", "Requested value '100' was not found.")]
        [InlineData("Value2", "Requested value 'Value2' was not found.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
