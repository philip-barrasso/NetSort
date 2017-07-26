using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSort.Tests.Models
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

        [Sortable("nonSortAddress")]
        public NonIComparableAddress NonSortableAddress { get; set; }

        [Sortable("joinDate", SortDirection.Desc)]
        public DateTime DateJoined { get; set; }
    }
}
