using System;
using Xunit;
using NetSort;
using System.Collections.Generic;
using System.Collections;

namespace NetSort.UnitTests
{
    public class SortOperationMetadataFinderTests
    {
        [Theory]
        [ClassData(typeof(FindMetadataTestDataGenerator))]
        public void SortOperationMetadataFinder_Find(string key, SortDirection? direction, string expectedPropertyName, 
                                                     SortDirection expectedDirection)
        {
            SortOperationMetadata outcome = SortOperationMetadataFinder.Find<Person>(key, direction);

            Assert.Equal(expectedPropertyName, outcome.ToSortBy.Name);
            Assert.Equal(expectedDirection, outcome.Direction);
        }

        public class FindMetadataTestDataGenerator : IEnumerable<object[]>
		{
			private List<object[]> _data = new List<object[]>()
			{
				new object[] { "age", SortDirection.Desc, "Age", SortDirection.Desc },
                new object[] { "age", null, "Age", SortDirection.Asc },
                new object[] { "nameDesc", null, "NameWithDefaultDescending", SortDirection.Desc },
                new object[] { "complexAddress", SortDirection.Asc, "Address", SortDirection.Asc }
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
