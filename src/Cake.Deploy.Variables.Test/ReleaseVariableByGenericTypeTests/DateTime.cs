using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Cake.Deploy.Variables.Test.ReleaseVariableByGenericTypeTests
{
    public class DateTime : ReleaseVariableByGenericTypeTests<System.DateTime>
    {
        [Theory]
        [ClassData(typeof(DateTimeSuccessTestData))]
        public override void AssertSuccess(string variableValue, System.DateTime expectedValue)
            => base.AssertSuccess(variableValue, expectedValue);

        [Theory]
        [ClassData(typeof(DateTimeFailureTestData))]
        public override void AssertFailure(string variableValue, string expectedErrorMessage)
            => base.AssertFailure(variableValue, expectedErrorMessage);
    }

    public class DateTimeSuccessTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "2019.05.23", new System.DateTime(2019, 5, 23) };
            yield return new object[] { "2019/05/23", new System.DateTime(2019, 5, 23) };
            yield return new object[] { "2019-05-23", new System.DateTime(2019, 5, 23) };
            yield return new object[] { "2019 05 23", new System.DateTime(2019, 5, 23) };
            yield return new object[] { "Friday, 29 May 2015", new System.DateTime(2015, 5, 29) };
            yield return new object[] { "Friday, 29 May 2015 05:50", new System.DateTime(2015, 5, 29, 5, 50, 0) };
            yield return new object[] { "Friday, 29 May 2015 05:50 AM", new System.DateTime(2015, 5, 29, 5, 50, 0) };
            yield return new object[] { "05/29/2015 05:50", new System.DateTime(2015, 5, 29, 5, 50, 0) };
            yield return new object[] { "2015-05-16T05:50:06", new System.DateTime(2015, 5, 16, 5, 50, 6) };
            yield return new object[] { "2015 May", new System.DateTime(2015, 5, 1) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class DateTimeFailureTestData : IEnumerable<object[]>
    {
        private const string ErrorMessage = "String was not recognized as a valid DateTime.";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "23.05.2019", ErrorMessage };
            yield return new object[] { "23 05 2019", ErrorMessage };
            yield return new object[] { "ABCSDSDSDSD#RE#", "The string was not recognized as a valid DateTime. There is an unknown word starting at index 0." };
            yield return new object[] { "23052019", ErrorMessage };
            yield return new object[] { "05232019", ErrorMessage };
            yield return new object[] { "Friday", ErrorMessage };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
