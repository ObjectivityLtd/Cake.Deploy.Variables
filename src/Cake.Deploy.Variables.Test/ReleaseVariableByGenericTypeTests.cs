using System;
using Cake.Core;
using FluentAssertions;
using Xunit;

namespace Cake.Deploy.Variables.Test
{
    public class ReleaseVariableByGenericTypeTests : IDisposable
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void When_VariableIsCorrectBoolean_Should_ReturnAppropriateValue(string variableValue, bool expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<bool>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("23423", "String was not recognized as a valid Boolean.")]
        [InlineData("T", "String was not recognized as a valid Boolean.")]
        public void When_VariableIsIncorrectBoolean_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<bool>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("f", 'f')]
        [InlineData("9", '9')]
        public void When_VariableIsCorrectChar_Should_ReturnAppropriateValue(string variableValue, char expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<char>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("", "String must be exactly one character long.")]
        [InlineData("someLongValue", "String must be exactly one character long.")]
        public void When_VariableIsIncorrectChar_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<char>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("-30", -30)]
        [InlineData("40", 40)]
        public void When_VariableIsCorrectSbyte_Should_ReturnAppropriateValue(string variableValue, sbyte expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<sbyte>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-1000", "Value was either too large or too small for a signed byte.")]
        [InlineData("2000", "Value was either too large or too small for a signed byte.")]
        [InlineData("ABCD", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectSbyte_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<sbyte>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("40", 40)]
        [InlineData("0", 0)]
        public void When_VariableIsCorrectByte_Should_ReturnAppropriateValue(string variableValue, byte expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<byte>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-30", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("-1000", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("2000", "Value was either too large or too small for an unsigned byte.")]
        [InlineData("ABCD", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectByte_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<byte>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("-100", -100)]
        [InlineData("4000", 4000)]
        public void When_VariableIsCorrectShort_Should_ReturnAppropriateValue(string variableValue, short expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<short>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("70000", "Value was either too large or too small for an Int16.")]
        [InlineData("-70000", "Value was either too large or too small for an Int16.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectShort_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<short>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("5000", 5000)]
        [InlineData("65535", 65535)]
        public void When_VariableIsCorrectUshort_Should_ReturnAppropriateValue(string variableValue, ushort expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<ushort>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-1000", "Value was either too large or too small for a UInt16.")]
        [InlineData("-30535", "Value was either too large or too small for a UInt16.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectUShort_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<ushort>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-85000", -85000)]
        public void When_VariableIsCorrectInt_Should_ReturnAppropriateValue(string variableValue, int expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<int>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("-4000000000", "Value was either too large or too small for an Int32.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectInt_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<int>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("4294967295", 4294967295)]
        public void When_VariableIsCorrectUint_Should_ReturnAppropriateValue(string variableValue, uint expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<uint>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-100", "Value was either too large or too small for a UInt32.")]
        [InlineData("8000000000", "Value was either too large or too small for a UInt32.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectUint_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<uint>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("-2147483647", -2147483647)]
        [InlineData("9223372036854775807", 9223372036854775807)]
        public void When_VariableIsCorrectLong_Should_ReturnAppropriateValue(string variableValue, long expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<long>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("18446744073709551615", "Value was either too large or too small for an Int64.")]
        [InlineData("-10000000000000000000", "Value was either too large or too small for an Int64.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectLong_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<long>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("18446744073709551615", 18446744073709551615)]
        [InlineData("4294967295", 4294967295)]
        public void When_VariableIsCorrectUlong_Should_ReturnAppropriateValue(string variableValue, ulong expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<ulong>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-100", "Value was either too large or too small for a UInt64.")]
        [InlineData("20000000000000000000", "Value was either too large or too small for a UInt64.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectUlong_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<ulong>(variableName), variableValue, expectedErrorMessage);

        [Theory]
        [InlineData("3.14", 3.14)]
        [InlineData("-90.1234", -90.1234)]
        [InlineData("1.23456784789", 1.23456784789)]
        [InlineData("123456.789798", 123456.789798)]
        //[InlineData("123,567", 123.567)]
        [InlineData("123.567", 123.567)]
        public void When_VariableIsCorrectFloat_Should_ReturnAppropriateValue(string variableValue, float expectedValue)
            => AssertSuccess((context, variableName) => context.ReleaseVariable<float>(variableName), variableValue, expectedValue);

        [Theory]
        [InlineData("-14.14,56", "Input string was not in a correct format.")]
        [InlineData("!@#$", "Input string was not in a correct format.")]
        public void When_VariableIsIncorrectFloat_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
            => AssertFailure((context, variableName) => context.ReleaseVariable<float>(variableName), variableValue, expectedErrorMessage);

        //[Theory]
        //[InlineData("", )]
        //[InlineData("", )]
        //public void When_VariableIsCorrect_Should_ReturnAppropriateValue(string variableValue,  expectedValue)
        //    => AssertSuccess((context, variableName) => context.ReleaseVariable<>(variableName), variableValue, expectedValue);

        //[Theory]
        //[InlineData("", "")]
        //[InlineData("", "")]
        //public void When_VariableIsIncorrect_Should_ThrowAnException(string variableValue, string expectedErrorMessage)
        //    => AssertFailure((context, variableName) => context.ReleaseVariable<>(variableName), variableValue, expectedErrorMessage);

        // TODO:
        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        private static void AssertSuccess<T>(ReleaseVariableAction<T> releaseVariableAction, string variableValue, T expectedValue) where T : IConvertible
        {
            // arrange
            const string currentEnvironment = "dev";
            var context = new CakeContextFixture(currentEnvironment).GetContext();

            const string variableName = "SomeValueName";
            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(variableName, variableValue);

            // act
            var value = releaseVariableAction(context, variableName);

            // assert
            value.Should().Be(expectedValue);
        }

        private static void AssertFailure(ReleaseVariableAction releaseVariableAction, string variableValue, string expectedErrorMessage)
        {
            // arrange
            const string currentEnvironment = "dev";
            var context = new CakeContextFixture(currentEnvironment).GetContext();

            const string variableName = "SomeValueName";
            context.ReleaseEnvironment(currentEnvironment)
                .AddVariable(variableName, variableValue);

            // act
            Action invalidAction = () => releaseVariableAction(context, variableName);

            // assert
            invalidAction.Should().Throw<Exception>().WithMessage(expectedErrorMessage);
        }

        public void Dispose()
        {
            VariableManager.Clear();
        }

        private delegate void ReleaseVariableAction(ICakeContext context, string variableName);
        private delegate T ReleaseVariableAction<out T>(ICakeContext context, string variableName);
    }
}
