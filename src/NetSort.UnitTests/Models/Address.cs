using System;

namespace NetSort.UnitTests.Models
{
    public class Address : IComparable
    {
        public string Street { get; set; }

        public string City { get; set; }

        [Sortable("state")]
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

        public int CompareTo(object obj)
        {
            var other = obj as Address;
            if (other == null) throw new ArgumentException("Address can only be compared to other Addresses");

            return string.CompareOrdinal(FullAddress, other.FullAddress);
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }
    }
}
