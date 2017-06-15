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
		public void DoesSortKeyValidatorWork(string key, bool expectedOutput)
		{
            var sut = new SortKeyValidator();
            bool isValid = sut.IsKeyValid<Person>(key);

            Assert.Equal(expectedOutput, isValid);
		}

        [Theory]
        [ClassData(typeof(ValidateKeyWithCustomDelimiterTestDataGenerator))]
		public void DoesSortKeyValidatorWithCustomDelimiterWork(string key, char delimiter, bool expectedOutput)
		{
            var sut = new SortKeyValidator();
            bool isValid = sut.IsKeyValid<Person>(key, delimiter);

            Assert.Equal(expectedOutput, isValid);
		}

		public class ValidateKeyTestDataGenerator : IEnumerable<object[]>
		{
			private List<object[]> _data = new List<object[]>()
			{
				new object[] { "some key that does not exist", false },
                new object[] { "age", true },
                new object[] { "complexAddress", true },
                new object[] { "nonSortAddress", false },
                new object[] { "nonSortAddress.zip", true },
                new object[] { "nonSortAddress.metadata", false }
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

        public class ValidateKeyWithCustomDelimiterTestDataGenerator : IEnumerable<object[]>
		{
			private List<object[]> _data = new List<object[]>()
			{
                new object[] { "nonSortAddress_zip", '_', true },
                new object[] { "nonSortAddress.zip", '.', true },
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
