using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSort.Tests.Models
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
