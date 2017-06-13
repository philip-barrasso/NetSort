using System;

namespace NetSort.UnitTests
{
    public class NonSortableAddress
    {
        [Sortable("metadata")]
        public AddressMeta Metadata { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Sortable("zip", SortDirection.Desc)]
        public string Zip { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{Street} {City}, {State} {Zip}";
            }
        }
    }
}
