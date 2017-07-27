using System;
using System.Collections.Generic;
using System.Text;

namespace NetSort.UnitTests.Models
{
    public class NonIComparableAddress
    {
        [Sortable("metadata")]
        public AddressMetadata Metadata { get; set; }

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
