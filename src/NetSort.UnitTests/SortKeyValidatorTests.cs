using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using NetSort.Validation;

namespace NetSort.UnitTests
{
    public class SortKeyValidatorTests
    {
        [Theory]
        [ClassData(typeof(ValidateKeyTestDataGenerator))]
		public void DoesSortWithBadKeyReturnFalse(string key, bool expectedOutput)
		{
            var sut = new SortKeyValidator();
            bool isValid = sut.IsKeyValid<Person>(key);

            Assert.Equal(expectedOutput ,isValid);
		}

		public class ValidateKeyTestDataGenerator : IEnumerable<object[]>
		{
			private List<object[]> _data = new List<object[]>()
			{
				new object[] { "some key that does not exist", false },
                new object[] { "age", true },
                new object[] { "complexAddress", true },
			};

			IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
			{
				return _data.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return _data.GetEnumerator();
			}
		}
    }
}
