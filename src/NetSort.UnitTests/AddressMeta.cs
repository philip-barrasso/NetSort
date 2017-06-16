using System;

namespace NetSort.UnitTests
{
    public class AddressMeta
    {
        public string SomeField { get; set; }

        [Sortable("decimal")]
        public decimal SomeDecimal { get; set; }
    }
}
