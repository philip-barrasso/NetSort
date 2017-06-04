using System;
namespace NetSort.UnitTests
{
    public class Person
    {
        [Sortable("age")]
        public int Age { get; set; }

        [Sortable("name")]
        public string Name { get; set; }

        [Sortable("worth")]
        public decimal NetWorth { get; set; }

        [Sortable("nameDesc", SortDirection.Desc)]
        public string NameWithDefaultDescending { get; set; }

        [Sortable("complexAddress")]
        public Address Address { get; set; }

        [Sortable("joinDate", SortDirection.Desc)]
        public DateTime DateJoined { get; set; }
    }
}
