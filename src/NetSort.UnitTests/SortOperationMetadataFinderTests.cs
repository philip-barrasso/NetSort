using System;
using Xunit;
using NetSort;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace NetSort.UnitTests
{
    public class SortOperationMetadataFinderTests
    {
        [Theory]
        [ClassData(typeof(FindMetadataTestDataGenerator))]
        public void SortOperationMetadataFinder_Find(string key, SortDirection? direction, IList<Tuple<string, SortDirection>> expectedOutcome)
        {
            IEnumerable<SortOperationMetadata> outcome = SortOperationMetadataFinder.Find<Person>(key, Constants.DEFAULT_NESTED_PROP_DELIMITER, direction);

            string[] keyParts = key.Split('.');
            Assert.Equal(keyParts.Length, outcome.Count());

            for (int index = 0; index < outcome.Count(); index++)
            {
                Assert.Equal(expectedOutcome[index].Item1, outcome.ToList()[index].ToSortBy.Name);
                Assert.Equal(expectedOutcome[index].Item2, outcome.ToList()[index].Direction);
            }
        }

        public class FindMetadataTestDataGenerator : IEnumerable<object[]>
		{
			private List<object[]> _data = new List<object[]>()
			{
				new object[] 
                { 
                    "age", 
                    SortDirection.Desc, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("Age", SortDirection.Desc) 
                    } 
                },

                new object[] 
                { 
                    "age", 
                    null, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("Age", SortDirection.Asc) 
                    } 
                },

                new object[] 
                { 
                    "nameDesc", 
                    null, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("NameWithDefaultDescending", SortDirection.Desc) 
                    } 
                },

                new object[] 
                { 
                    "complexAddress", 
                    SortDirection.Asc, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("Address", SortDirection.Asc) 
                    } 
                },

                new object[] 
                { 
                    "complexAddress.state", 
                    null, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("Address", SortDirection.Asc),
                        new Tuple<string, SortDirection>("State", SortDirection.Asc) 
                    } 
                },

                new object[] 
                { 
                    "complexAddress.zip", 
                    null, 
                    new List<Tuple<string, SortDirection>>() 
                    { 
                        new Tuple<string, SortDirection>("Address", SortDirection.Asc),
                        new Tuple<string, SortDirection>("Zip", SortDirection.Desc) 
                    } 
                },
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
