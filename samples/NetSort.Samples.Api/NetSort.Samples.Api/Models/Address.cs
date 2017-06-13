using NetSort;
using System;

namespace NetSort.Samples.Api.Models
{
    public class Address
    {
        [Sortable("state")]
        public string State { get; set; }

        [Sortable("city")]
        public string City { get; set; }

        [Sortable("street")]
        public string Street { get; set; }

        [Sortable("zip", SortDirection.Desc)]
        public string Zip { get; set; }
    }
}