    using System;
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections;

    namespace NetSort.UnitTests
    {
    public class IEnumerableSortingTests
    {
        [Theory]
        [ClassData(typeof(SortWithStringDirectionTestDataGenerator))]
        public void DoesSortWithStringDirectionWork(IEnumerable<Person> people, string sortKey, string direction,
                                                    IList<Person> expectedOutput, Func<Person, int> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey, direction).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByNestedPropertyTestDataGenerator))]
        public void DoesSortWithNestedPropertyWork(IEnumerable<Person> people, string sortKey,
                                                    IList<Person> expectedOutput, Func<Person, string> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByIntTestDataGenerator))]
        public void DoesSortByIntWork(IEnumerable<Person> people, string sortKey, IList<Person> expectedOutput, 
                                        Func<Person, int> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByDateTimeTestDataGenerator))]
        public void DoesSortByDateTimeWork(IEnumerable<Person> people, string sortKey, IList<Person> expectedOutput,
                                            SortDirection? direction, Func<Person, DateTime> equalitySelector)
        {
            IEnumerable<Person> sortedPeople;
            if (direction != null)
            {
                sortedPeople = people.SortByKey(sortKey, direction.Value);
            }
            else
            {
                sortedPeople = people.SortByKey(sortKey);
            }

            bool isCorrect = DoesMatch(sortedPeople.ToList(), expectedOutput, equalitySelector);
            Assert.True(isCorrect);			
        }

        [Theory]
        [ClassData(typeof(SortByStringTestDataGenerator))]
        public void DoesSortByStringWork(IEnumerable<Person> people, string sortKey, IList<Person> expectedOutput, 
                                            Func<Person, string> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByDecimalTestDataGenerator))]
        public void DoesSortByDecimalWork(IEnumerable<Person> people, string sortKey, IList<Person> expectedOutput, 
                                            Func<Person, decimal> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByIntDescendingTestDataGenerator))]
        public void DoesSortWithOverrideDirectionWork(IEnumerable<Person> people, string sortKey, 
                                                        IList<Person> expectedOutput, Func<Person, int> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey, SortDirection.Desc).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByStringDescendingTestDataGenerator))]
        public void DoesSortWithDefaultDirectionOnAttributeWork(IEnumerable<Person> people, string sortKey, 
                                                                IList<Person> expectedOutput, 
                                                                Func<Person, string> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Theory]
        [ClassData(typeof(SortByComplexObjectTestDataGenerator))]
        public void DoesSortByComplexObjectWork(IEnumerable<Person> people, string sortKey, IList<Person> expectedOutput,
                                                Func<Person, Address> equalitySelector)
        {
            var sortedPeople = people.SortByKey(sortKey).ToList();
            bool isCorrect = DoesMatch(sortedPeople, expectedOutput, equalitySelector);

            Assert.True(isCorrect);
        }

        [Fact]
        public void DoesSortWithBadKeyThrowException()
        {
            Assert.Throws<ArgumentException>(() => new List<Person>().SortByKey("something that doesn't exist"));
        }

        [Fact]
        public void DoesSortWithBadDirectionThrowException()
        {
            Assert.Throws<ArgumentException>(() => new List<Person>().SortByKey("age", "some direction"));
        }

        private bool DoesMatch<T, TEQuality>(IList<T> data1, IList<T> data2, Func<T, TEQuality> equalitySelector)
        {
            if (data1.Count != data2.Count) return false;
            for (int index = 0; index < data1.Count; index++)
            {
                TEQuality d1Val = equalitySelector(data1[index]);
                TEQuality d2Val = equalitySelector(data2[index]);

                if (d1Val.Equals(d2Val) == false) return false;
            }

            return true;
        }

        public class SortByComplexObjectTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Address = new Address() { City = "Atlanta", State = "GA", Street = "123 Main" } },
                        new Person() { Address = new Address() { City = "Atlanta", State = "GA", Street = "122 Main" } },
                        new Person() { Address = new Address() { State = "GA" } },
                    },
                    "complexAddress",
                    new List<Person>()
                    {
                        new Person() { Address = new Address() { State = "GA" } },
                        new Person() { Address = new Address() { City = "Atlanta", State = "GA", Street = "122 Main" } },
                        new Person() { Address = new Address() { City = "Atlanta", State = "GA", Street = "123 Main" } },
                    },
                    new Func<Person, Address>(p => p.Address)
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

        public class SortByIntTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    new List<Person>()
                    {
                        new Person() { Age = 2 },
                        new Person() { Age = 3 },
                        new Person() { Age = 5 },
                    },
                    new Func<Person, int>(p => p.Age)
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

        public class SortWithStringDirectionTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    "Asc",
                    new List<Person>()
                    {
                        new Person() { Age = 2 },
                        new Person() { Age = 3 },
                        new Person() { Age = 5 },
                    },
                    new Func<Person, int>(p => p.Age)
                },
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    "deSc",
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    new Func<Person, int>(p => p.Age)
                },
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    "desc",
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    new Func<Person, int>(p => p.Age)
                },
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    "ASC",
                    new List<Person>()
                    {
                        new Person() { Age = 2 },
                        new Person() { Age = 3 },
                        new Person() { Age = 5 },
                    },
                    new Func<Person, int>(p => p.Age)
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

        public class SortByDateTimeTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { DateJoined = DateTime.Now.AddDays(-5).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-2).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-10).Date },
                    },
                    "joinDate",
                    new List<Person>()
                    {
                        new Person() { DateJoined = DateTime.Now.AddDays(-10).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-5).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-2).Date },
                    },
                    SortDirection.Asc,
                    new Func<Person, DateTime>(p => p.DateJoined)
                },
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { DateJoined = DateTime.Now.AddDays(-5).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-2).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-10).Date },
                    },
                    "joinDate",
                    new List<Person>()
                    {
                        new Person() { DateJoined = DateTime.Now.AddDays(-2).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-5).Date },
                        new Person() { DateJoined = DateTime.Now.AddDays(-10).Date },
                    },
                    null,
                    new Func<Person, DateTime>(p => p.DateJoined)
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

        public class SortByIntDescendingTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Age = 3 },
                        new Person() { Age = 5 },
                        new Person() { Age = 2 },
                    },
                    "age",
                    new List<Person>()
                    {
                        new Person() { Age = 5 },
                        new Person() { Age = 3 },
                        new Person() { Age = 2 },
                    },
                    new Func<Person, int>(p => p.Age)
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

        public class SortByNestedPropertyTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Address = new Address() { City = "Atl", State = "GA", Zip = "30308" } },
                        new Person() { Address = new Address() { City = "Wich", State = "KS", Zip = "67202" } },
                        new Person() { Address = new Address() { City = "Mem", State = "TN", Zip = "78211" } },
                    },
                    "complexAddress.zip",
                    new List<Person>()
                    {
                        new Person() { Address = new Address() { City = "Mem", State = "TN", Zip = "78211" } },
                        new Person() { Address = new Address() { City = "Wich", State = "KS", Zip = "67202" } },
                        new Person() { Address = new Address() { City = "Atl", State = "GA", Zip = "30308" } },
                    },
                    new Func<Person, string>(p => p.Address.Zip)
                },

                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Address = new Address() { City = "Atl", State = "GA", Zip = "30308" } },
                        new Person() { Address = new Address() { City = "Wich", State = "KS", Zip = "67202" } },
                        new Person() { Address = new Address() { City = "Mem", State = "TN", Zip = "78211" } },
                    },
                    "complexAddress.state",
                    new List<Person>()
                    {											
                        new Person() { Address = new Address() { City = "Atl", State = "GA", Zip = "30308" } },
                        new Person() { Address = new Address() { City = "Wich", State = "KS", Zip = "67202" } },
                        new Person() { Address = new Address() { City = "Mem", State = "TN", Zip = "78211" } },
                    },
                    new Func<Person, string>(p => p.Address.State)
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

        public class SortByStringTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { Name = "Bob" },
                        new Person() { Name = "Steve" },
                        new Person() { Name = "Andrew" },
                    },
                    "name",
                    new List<Person>()
                    {
                        new Person() { Name = "Andrew" },
                        new Person() { Name = "Bob" },
                        new Person() { Name = "Steve" },
                    },
                    new Func<Person, string>(p => p.Name)
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

        public class SortByStringDescendingTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { NameWithDefaultDescending = "Bob" },
                        new Person() { NameWithDefaultDescending = "Steve" },
                        new Person() { NameWithDefaultDescending = "Andrew" },
                    },
                    "nameDesc",
                    new List<Person>()
                    {
                        new Person() { NameWithDefaultDescending = "Steve" },
                        new Person() { NameWithDefaultDescending = "Bob" },
                        new Person() { NameWithDefaultDescending = "Andrew" },
                    },
                    new Func<Person, string>(p => p.NameWithDefaultDescending)
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

        public class SortByDecimalTestDataGenerator : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>()
            {
                new object[]
                {
                    new List<Person>()
                    {
                        new Person() { NetWorth = 123.55m },
                        new Person() { NetWorth = 123.54m },
                        new Person() { NetWorth = 121 },
                    },
                    "worth",
                    new List<Person>()
                    {
                        new Person() { NetWorth = 121 },
                        new Person() { NetWorth = 123.54m },
                        new Person() { NetWorth = 123.55m },
                    },
                    new Func<Person, decimal>(p => p.NetWorth),
                }
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