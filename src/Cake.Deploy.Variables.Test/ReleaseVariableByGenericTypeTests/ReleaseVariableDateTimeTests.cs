namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    public class ReleaseVariableDateTimeTests : ReleaseVariableGenericBaseTests<System.DateTime>
    {
        public static IEnumerable<object[]> DateTimeSuccessTestData => new List<object[]>
        {
            new object[] { "2019.05.23", new System.DateTime(2019, 5, 23) },
            new object[] { "2019/05/23", new System.DateTime(2019, 5, 23) },
            new object[] { "2019-05-23", new System.DateTime(2019, 5, 23) },
            new object[] { "2019 05 23", new System.DateTime(2019, 5, 23) },
            new object[] { "Friday, 29 May 2015", new System.DateTime(2015, 5, 29) },
            new object[] { "Friday, 29 May 2015 05:50", new System.DateTime(2015, 5, 29, 5, 50, 0) },
            new object[] { "Friday, 29 May 2015 05:50 AM", new System.DateTime(2015, 5, 29, 5, 50, 0) },
            new object[] { "05/29/2015 05:50", new System.DateTime(2015, 5, 29, 5, 50, 0) },
            new object[] { "2015-05-16T05:50:06", new System.DateTime(2015, 5, 16, 5, 50, 6) },
            new object[] { "2015 May", new System.DateTime(2015, 5, 1) },
        };

        public static IEnumerable<object[]> DateTimeFailureTestData
        {
            get
            {
                yield return CreateFailureTestDataItem("23.05.2019");
                yield return CreateFailureTestDataItem("23 05 2019");
                yield return new object[] { "ABCSDSDSDSD#RE#", "The string 'ABCSDSDSDSD#RE#' was not recognized as a valid DateTime. There is an unknown word starting at index '0'." };
                yield return CreateFailureTestDataItem("23052019");
                yield return CreateFailureTestDataItem("05232019");
                yield return CreateFailureTestDataItem("Friday");
            }
        }

        [Theory]
        [MemberData(nameof(DateTimeSuccessTestData))]
        public override void AssertSuccess(string variableValue, System.DateTime expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [MemberData(nameof(DateTimeFailureTestData))]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);

        private static object[] CreateFailureTestDataItem(string value) => new[] { value, $"String '{value}' was not recognized as a valid DateTime." };
    }
}
