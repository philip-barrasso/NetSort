using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetSort.UnitTests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetSort.UnitTests
{
    [TestClass]
    public class SortableExtensionsTests
    {
        [TestMethod]
        public void SortableExtensions_SortByKey_SimpleSort_HasReasonablePerformance()
        {
            var people = new List<Person>();
            for (var index = 0; index < 10000000; index++)
            {
                people.Add(new Person() { Age = index % 100 });
            }

            var sw = new Stopwatch();
            sw.Start();

            var sortedPeople = people.SortByKey("age");
            sw.Stop();

            var isReasonable = sw.ElapsedMilliseconds <= 500;

            Assert.IsTrue(isReasonable);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_NestedSort_HasReasonablePerformance()
        {
            var people = new List<Person>();
            for (var index = 0; index < 1000000; index++)
            {
                people.Add(new Person()
                {
                    Age = index % 100,
                    NonSortableAddress = new NonIComparableAddress()
                    {
                        Metadata = new AddressMetadata() { SomeDecimal = index },
                    }
                });
            }

            var sw = new Stopwatch();
            sw.Start();

            var sortedPeople = people.SortByKey("nonSortAddress.metadata.decimal");
            sw.Stop();

            var isReasonable = sw.ElapsedMilliseconds <= 500;

            Assert.IsTrue(isReasonable);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_ComplexObjectSort_HasReasonablePerformance()
        {
            var people = new List<Person>();
            for (var index = 0; index < 1000000; index++)
            {
                people.Add(new Person()
                {
                    Age = index % 100,
                    Address = new Address
                    {
                        Street = (index % 100).ToString(),
                        City = (index % 100).ToString(),
                        State = (index % 100).ToString(),
                        Zip = (index % 100).ToString(),
                    },
                });
            }

            var sw = new Stopwatch();
            sw.Start();

            var sortedPeople = people.SortByKey("complexAddress");
            sw.Stop();

            var isReasonable = sw.ElapsedMilliseconds <= 500;

            Assert.IsTrue(isReasonable);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithStringDirection_Works()
        {
            var people = new List<Person>
            {
                new Person { Name = "Bob" },
                new Person { Name = "Zan" },
                new Person { Name = "Tom" },
                new Person { Name = "Bob3" },
            };

            var sortedPeople = people.SortByKey("name", "asc").ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderBy(p => p.Name).ToList(), p => p.Name);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithStringDirection_ShouldIgnoreDirectionCasing()
        {
            var people = new List<Person>
            {
                new Person { Name = "Bob" },
                new Person { Name = "Zan" },
                new Person { Name = "Tom" },
                new Person { Name = "Bob3" },
            };

            var sortedPeople = people.SortByKey("name", "dEsC").ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderByDescending(p => p.Name).ToList(), p => p.Name);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithOverrideDirection_Works()
        {
            var people = new List<Person>
            {
                new Person { Name = "Bob" },
                new Person { Name = "AZan" },
                new Person { Name = "Tom" },
                new Person { Name = "Bob3" },
            };

            var sortedPeople = people.SortByKey("name", SortDirection.Desc).ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderByDescending(p => p.Name).ToList(), p => p.Name);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithDefaultDirectionOnAttribute_Works()
        {
            var people = new List<Person>
            {
                new Person { NameWithDefaultDescending = "Bob" },
                new Person { NameWithDefaultDescending = "AZan" },
                new Person { NameWithDefaultDescending = "Tom" },
                new Person { NameWithDefaultDescending = "Bob3" },
            };

            var sortedPeople = people.SortByKey("nameDesc").ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderByDescending(p => p.NameWithDefaultDescending).ToList(), p => p.NameWithDefaultDescending);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithDefaultDirectionOnAttributeAndOverrideDirection_ShouldUseOverrideDirection()
        {
            var people = new List<Person>
            {
                new Person { NameWithDefaultDescending = "Bob" },
                new Person { NameWithDefaultDescending = "AZan" },
                new Person { NameWithDefaultDescending = "Tom" },
                new Person { NameWithDefaultDescending = "Bob3" },
            };

            var sortedPeople = people.SortByKey("nameDesc", SortDirection.Asc).ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderBy(p => p.NameWithDefaultDescending).ToList(), p => p.NameWithDefaultDescending);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SortableExtensions_SortByKey_WithBadKey_ShouldThrowsArgumentOutOfRangeException()
        {
            var people = new List<Person>();
            var sortedPeople = people.SortByKey("someKeyThatDoesntExist", SortDirection.Desc).ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SortableExtensions_SortByKey_WithBadDirection_ShouldThrowsArgumentOutOfRangeException()
        {
            var people = new List<Person>
            {
                new Person { Name = "Bob" },
                new Person { Name = "AZan" },
                new Person { Name = "Tom" },
                new Person { Name = "Bob3" },
            };

            var sortedPeople = people.SortByKey("name", "someDirectionThatDoesntExist").ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SortableExtensions_SortByKey_WithNullKey_ShouldThrowsArgumentOutOfRangeException()
        {
            var people = new List<Person>
            {
                new Person { NameWithDefaultDescending = "Bob" },
                new Person { NameWithDefaultDescending = "AZan" },
                new Person { NameWithDefaultDescending = "Tom" },
                new Person { NameWithDefaultDescending = "Bob3" },
            };

            var sortedPeople = people.SortByKey(null).ToList();

            var isCorrect = DoesMatch(sortedPeople, people.ToList(), p => p.NameWithDefaultDescending);

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]        
        public void SortableExtensions_SortByKey_WithNullData_Works()
        {
            List<Person> people = null;
            var sortedPeople = people.SortByKey("nameDesc");

            Assert.AreEqual(null, sortedPeople);
        }

        [TestMethod]
        public void SortableExtensions_SortByKey_WithCustomDelimiter_Works()
        {
            var people = new List<Person>
            {
                new Person { NonSortableAddress = new NonIComparableAddress { Metadata = new AddressMetadata { SomeDecimal = 5, } } },
                new Person { NonSortableAddress = new NonIComparableAddress { Metadata = new AddressMetadata { SomeDecimal = 8, } } },
                new Person { NonSortableAddress = new NonIComparableAddress { Metadata = new AddressMetadata { SomeDecimal = 4, } } },
                new Person { NonSortableAddress = new NonIComparableAddress { Metadata = new AddressMetadata { SomeDecimal = 2, } } },
            };

            var sortedPeople = people.SortByKey("nonSortAddress_metadata_decimal", '_').ToList();

            var isCorrect = DoesMatch(sortedPeople, people.OrderBy(p => p.NonSortableAddress.Metadata.SomeDecimal).ToList(), p => p.NonSortableAddress.Metadata.SomeDecimal);

            Assert.IsTrue(isCorrect);
        }

        private bool DoesMatch<T, TEQuality>(IList<T> data1, IList<T> data2, Func<T, TEQuality> equalitySelector)
        {
            if (data1.Count != data2.Count) return false;

            for (var index = 0; index < data1.Count; index++)
            {
                var d1Val = equalitySelector(data1[index]);
                var d2Val = equalitySelector(data2[index]);

                if (!d1Val.Equals(d2Val)) return false;
            }

            return true;
        }
    }
}
