﻿namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using Xunit;

    public class ReleaseVariableInt32Tests : ReleaseVariableGenericBaseTests<int>
    {
        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-85000", -85000)]
        public override void AssertSuccess(string variableValue, int expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [InlineData("4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("-4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }
}
