using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetSort.UnitTests.EF;
using System.Collections.Generic;
using System.Linq;

namespace NetSort.UnitTests
{
    [TestClass]
    public class IQueryableSortableExtensionsTests
    {
        [TestMethod]
        public void IQueryableSortableExtensionsTests_SimpleIQueryableSort_Works()
        {
            var data = new List<EF_Person>
            {
                new EF_Person { Age = 5, },
                new EF_Person { Age = 2, },
                new EF_Person { Age = 3, },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EF_Person>>();
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<FakeDbContext>();
            mockContext.Setup(c => c.EF_Persons).Returns(mockSet.Object);

            var people = mockContext.Object.EF_Persons
                .Where(p => p.Age > 0)
                .SortByKey("age")
                .Take(50)
                .ToList();

            Assert.AreEqual(2, people[0].Age);
        }

        [TestMethod]
        public void IQueryableSortableExtensionsTests_NestedIQueryableSort_Works()
        {
            var data = new List<EF_Person>
            {
                new EF_Person { Age = 5, Score = new EF_Score { Value = 4 } },
                new EF_Person { Age = 2, Score = new EF_Score { Value = 7 } },
                new EF_Person { Age = 3, Score = new EF_Score { Value = 3 } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EF_Person>>();
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<FakeDbContext>();
            mockContext.Setup(c => c.EF_Persons).Returns(mockSet.Object);

            var people = mockContext.Object.EF_Persons
                .Where(p => p.Age > 0)
                .SortByKey("score.value")
                .Take(50)
                .ToList();

            var peopleDescending = mockContext.Object.EF_Persons
                .Where(p => p.Age > 0)
                .SortByKey("score.value", SortDirection.Desc)
                .Take(50)
                .ToList();

            Assert.AreEqual(3, people[0].Score.Value);
            Assert.AreEqual(7, peopleDescending[0].Score.Value);
        }

        [TestMethod]
        public void IQueryableSortableExtensionsTests_ThenSortBy_Works()
        {
            var data = new List<EF_Person>
            {
                new EF_Person { Age = 5, Score = new EF_Score { Value = 4 } },
                new EF_Person { Age = 2, Score = new EF_Score { Value = 7 } },
                new EF_Person { Age = 3, Score = new EF_Score { Value = 7 } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EF_Person>>();
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<EF_Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<FakeDbContext>();
            mockContext.Setup(c => c.EF_Persons).Returns(mockSet.Object);

            var people = mockContext.Object.EF_Persons
                .Where(p => p.Age > 0)
                .SortByKey("score.value")
                .ThenSortByKey("age")
                .Take(50)
                .ToList();

            Assert.AreEqual(4, people[0].Score.Value);
            Assert.AreEqual(7, people[1].Score.Value);
            Assert.AreEqual(7, people[2].Score.Value);
            Assert.AreEqual(5, people[0].Age);
            Assert.AreEqual(2, people[1].Age);
            Assert.AreEqual(3, people[2].Age);

            var peopleDescending = mockContext.Object.EF_Persons
                .Where(p => p.Age > 0)
                .SortByKey("score.value")
                .ThenSortByKey("age", SortDirection.Desc)
                .Take(50)
                .ToList();

            Assert.AreEqual(4, peopleDescending[0].Score.Value);
            Assert.AreEqual(7, peopleDescending[1].Score.Value);
            Assert.AreEqual(7, peopleDescending[2].Score.Value);
            Assert.AreEqual(5, peopleDescending[0].Age);
            Assert.AreEqual(3, peopleDescending[1].Age);
            Assert.AreEqual(2, peopleDescending[2].Age);
        }
    }
}
